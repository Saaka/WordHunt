using WordHunt.Data.Entities;
using WordHunt.DataInterfaces.Words.DTO;
using WordHunt.DataInterfaces.Words.Request;

namespace WordHunt.Services.Words.Mapper
{
    public interface IWordMapper
    {
        Data.Entities.Word MapCreateRequest(WordCreateRequest wordCreateRequest);
        Data.Entities.Word MapWord(Data.Entities.Word word, WordUpdateRequest updateRequest);
    }
}