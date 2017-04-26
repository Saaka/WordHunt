using WordHunt.Data.Entities;
using WordHunt.Models.Words.Creation;
using WordHunt.Models.Words.Modification;

namespace WordHunt.Services.Words.Mapper
{
    public interface IWordMapper
    {
        Word MapWord(WordCreate wordCreateRequest);
        Word MapWord(Word word, WordUpdate updateRequest);
    }

    public class WordMapper : IWordMapper
    {
        public Word MapWord(WordCreate wordCreateRequest)
        {
            return new Word()
            {
                CategoryId = wordCreateRequest.CategoryId,
                LanguageId = wordCreateRequest.LanguageId,
                Value = wordCreateRequest.Value
            };
        }

        public Word MapWord(Word word, WordUpdate updateRequest)
        {
            word.CategoryId = updateRequest.CategoryId;
            word.Value = updateRequest.Value;

            return word;
        }
    }
}
