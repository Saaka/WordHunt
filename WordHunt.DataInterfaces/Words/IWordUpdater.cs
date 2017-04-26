using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Words.Request;
using WordHunt.Interfaces.Words.Result;

namespace WordHunt.Interfaces.Words
{
    public interface IWordUpdater
    {
        Task<WordUpdateResult> UpdateWord(WordUpdateRequest request);
    }
}
