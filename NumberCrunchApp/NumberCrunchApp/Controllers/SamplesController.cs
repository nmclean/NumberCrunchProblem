using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NumberCrunchApp.Models;
using NumberCrunchApp.Services;
using System.Net;

namespace NumberCrunchApp.Controllers {
    public class SamplesController : Controller {
        private readonly ILogger<SamplesController> _logger;
        private readonly NumberCrunchApiService _apiService;

        public SamplesController(ILogger<SamplesController> logger, NumberCrunchApiService apiService) {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult GenerateGroup() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateGroup(SampleGenerationViewModel input, CancellationToken cancellationToken) {
            if (!ModelState.IsValid) {
                return View(input);
            }

            string? description = null;

            if (input.DescriptionFile is not null) {
                using var reader = new StreamReader(input.DescriptionFile.OpenReadStream());
                description = await reader.ReadToEndAsync(cancellationToken);
            }

            var request = new GenerateSamplesRequest(input.SampleCount, input.PatientScore, input.DoctorScore, description);

            var newGroupId = await _apiService.GenerateSamples(request, cancellationToken);

            return RedirectToAction(nameof(ViewGroup), new { id = newGroupId });
        }

        public async Task<IActionResult> ViewGroup(int id, CancellationToken cancellationToken,
                [FromQuery] [Range(1, int.MaxValue)] int page = 1) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var data = await _apiService.GetSamples(id, page, cancellationToken);
            if (data is null) {
                return NotFound();
            }

            return View(new SampleGroupViewModel(data, page));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}