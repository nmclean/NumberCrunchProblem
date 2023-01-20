using System.ComponentModel.DataAnnotations;

namespace NumberCrunchApi.Data {
    public record GenerateSamplesRequest(
         [Range(1, int.MaxValue)] int Count,
         [Range(1, int.MaxValue)] int PatientScore,
         [Range(1, int.MaxValue)] int DoctorScore,
         string? Description
    );

    public record SampleResponse(string? Description, List<SampleResponseItem> Items);
    public record SampleResponseItem(int Number, bool MatchesPatient, bool MatchesDoctor);
}
    