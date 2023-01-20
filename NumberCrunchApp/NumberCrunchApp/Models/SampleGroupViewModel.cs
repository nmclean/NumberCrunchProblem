namespace NumberCrunchApp.Models {
    public class SampleGroupViewModel {
        private readonly SampleResponse _data;

        public string? Description => _data.Description;

        public int Page { get; }

        public IEnumerable<SampleViewModel> Items { get; }

        public SampleGroupViewModel(SampleResponse data, int page) {
            _data = data;
            Page = page;
            Items = _data.Items.Select(item => new SampleViewModel(item)).ToList();
        }
    }
}
