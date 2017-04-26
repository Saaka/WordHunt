namespace WordHunt.Models.Words.Modification
{
    public class WordUpdate
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? CategoryId { get; set; }
    }

    public class WordUpdateResult
    {
        public WordModel Word { get; set; }
    }
}
