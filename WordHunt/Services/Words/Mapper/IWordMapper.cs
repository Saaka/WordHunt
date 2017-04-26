using WordHunt.Data.Entities;
using WordHunt.Interfaces.Words.DTO;
using WordHunt.Interfaces.Words.Request;

namespace WordHunt.Services.Words.Mapper
{
    public interface IWordMapper
    {
        Data.Entities.Word MapCreateRequest(WordCreateRequest wordCreateRequest);
        Data.Entities.Word MapWord(Data.Entities.Word word, WordUpdateRequest updateRequest);
    }
}