using System.Threading.Tasks;
using WordHunt.WebAPI.Models;

namespace WordHunt.WebAPI.Auth.Token
{
    public interface ITokenGenerator
    {
        Task<TokenGeneratorResult> GenerateToken(string email, string password);
    }
}