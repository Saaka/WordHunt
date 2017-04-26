using System.Threading.Tasks;
using WordHunt.Interfaces.Words.Request;
using WordHunt.Interfaces.Words.Result;

namespace WordHunt.Interfaces.Words
{
    public interface IWordCreator
    {
        Task<WordCreateResult> CreateWord(WordCreateRequest wordCreate);
    }
}
