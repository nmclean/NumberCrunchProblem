namespace NumberCrunchApi.Data {
    public class Sample {
        public int SampleId { get; set; }

        public int GroupId {  get; set; }
        public virtual SampleGroup? Group { get; set; }
        
        public int Number { get; set; }
        public bool MatchesDoctor { get; set; }
        public bool MatchesPatient { get; set; }
    }
}