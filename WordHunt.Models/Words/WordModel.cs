namespace WordHunt.Models.Words
{
    public class WordModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
