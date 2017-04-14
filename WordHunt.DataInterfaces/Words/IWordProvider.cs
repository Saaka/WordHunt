using System.Threading.Tasks;
using WordHunt.DataInterfaces.Words.Request;
using WordHunt.DataInterfaces.Words.Result;

namespace WordHunt.DataInterfaces.Words
{
    public interface IWordProvider
    {
        Task<WordListGetResult> GetWordList(WordListGetRequest request);
        Task<WordGetResult> GetWord(long wordId);
    }
}
