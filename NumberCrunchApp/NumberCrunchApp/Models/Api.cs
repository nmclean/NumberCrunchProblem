using System.ComponentModel.DataAnnotations;

namespace NumberCrunchApp.Models {
    public record GenerateSamplesRequest(int Count, int PatientScore, int DoctorScore, string? Description);

    public record SampleResponse(string? Description, List<SampleResponseItem> Items);
    public record SampleResponseItem(int Number, bool MatchesPatient, bool MatchesDoctor);
}
