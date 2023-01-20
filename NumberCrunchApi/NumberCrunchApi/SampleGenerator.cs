using NumberCrunchApi.Data;

namespace NumberCrunchApi {
    public class SampleGenerator {
        public IEnumerable<Sample> Generate(int count, int patientScore, int doctorScore) {
            foreach (var number in Enumerable.Range(1, count)) {
                yield return new() {
                    Number = number,
                    MatchesPatient = number % patientScore == 0,
                    MatchesDoctor = number % doctorScore == 0
                };
            }
        }
    }
}
