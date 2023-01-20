using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberCrunchApi.Data;

namespace NumberCrunchApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class NumberCrunchController : ControllerBase {
        private readonly ILogger<NumberCrunchController> _logger;
        private readonly NumberCrunchDbContext _context;
        private readonly SampleGenerator _generator;

        public NumberCrunchController(ILogger<NumberCrunchController> logger, NumberCrunchDbContext context, SampleGenerator generator) {
            _logger = logger;
            _context = context;
            _generator = generator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateSamples([FromBody] GenerateSamplesRequest request, CancellationToken cancellationToken) {
            var group = new SampleGroup {
                Description = request.Description,
                Samples = _generator.Generate(request.Count, request.PatientScore, request.DoctorScore).ToList()
            };

            _context.SampleGroups.Add(group);

            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetSamples), new { id = group.SampleGroupId }, group.SampleGroupId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SampleResponse>> GetSamples(int id, CancellationToken cancellationToken,
                [FromQuery] [Range(1, int.MaxValue)] int start = 1,
                [FromQuery] [Range(1, 1000)] int count = 100) {
            var query =
                from sampleGroup in _context.SampleGroups
                where sampleGroup.SampleGroupId == id
                select new {
                    sampleGroup.Description,
                    Items =
                        from sample in sampleGroup.Samples!
                        where sample.Number >= start && sample.Number < start + count
                        select new { sample.Number, sample.MatchesPatient, sample.MatchesDoctor }
                };
            
            var result = await query.FirstOrDefaultAsync(cancellationToken);

            if (result is null) {
                return NotFound();
            }
            
            return new SampleResponse(
                result.Description,
                result.Items
                    .Select(item => new SampleResponseItem(item.Number, item.MatchesPatient, item.MatchesDoctor))
                    .ToList()
            );
        }
    }
}