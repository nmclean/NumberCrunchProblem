using System.ComponentModel.DataAnnotations;

namespace NumberCrunchApp.Models {
    public class SampleGenerationViewModel {
        [Range(1, int.MaxValue)]
        public int SampleCount { get; set; }

        [Range(1, int.MaxValue)]
        public int PatientScore { get; set; }

        [Range(1, int.MaxValue)]
        public int DoctorScore { get; set; }

        public IFormFile? DescriptionFile { get; set; }
    }
}
