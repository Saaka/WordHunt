using WordHunt.Data.Entities;
using WordHunt.DataInterfaces.Words.DTO;
using WordHunt.DataInterfaces.Words.Request;

namespace WordHunt.Data.Services.Words.Mapper
{
    public interface IWordMapper
    {
        Entities.Word MapCreateRequest(WordCreateRequest wordCreateRequest);
        Entities.Word MapWord(Entities.Word word, WordUpdateRequest updateRequest);
    }
}