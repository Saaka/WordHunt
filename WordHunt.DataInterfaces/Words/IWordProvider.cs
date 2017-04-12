using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Words.DTO.Access;

namespace WordHunt.DataInterfaces.Words
{
    public interface IWordProvider
    {
        Task<GetWordListResult> GetWordList(WordListRequest request); 
    }
}
