namespace NumberCrunchApi.Data {
    public class SampleGroup {
        public int SampleGroupId { get; set; }

        public virtual List<Sample>? Samples { get; set; }

        public string? Description { get; set; }
    }
}
