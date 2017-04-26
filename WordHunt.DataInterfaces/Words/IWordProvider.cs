using System.Threading.Tasks;
using WordHunt.Interfaces.Words.Request;
using WordHunt.Interfaces.Words.Result;

namespace WordHunt.Interfaces.Words
{
    public interface IWordProvider
    {
        Task<WordListGetResult> GetWordList(WordListGetRequest request);
        Task<WordGetResult> GetWord(int wordId);
    }
}
