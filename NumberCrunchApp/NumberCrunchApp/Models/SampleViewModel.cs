namespace NumberCrunchApp.Models {
    public class SampleViewModel {
        private readonly SampleResponseItem _data;
        
        public int Number => _data.Number;

        public string ScoreDisplay =>
            _data switch {
                { MatchesPatient: true, MatchesDoctor: true } => "Both",
                { MatchesPatient: true } => "Patient",
                { MatchesDoctor: true } => "Doctor",
                _ => "None"
            };

        public SampleViewModel(SampleResponseItem data) {
            _data = data;
        }
    }
}
