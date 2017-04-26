using System.Collections.Generic;

namespace WordHunt.Models.Words.Access
{
    public class WordListGet
    {
        public int LanguageId { get; set; }
        public int? CategoryId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public bool OrderByDesc { get; set; }
        public string Value { get; set; }
    }

    public class WordListGetResult
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<WordModel> WordList { get; set; }
    }
}
