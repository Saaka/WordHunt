using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Words.Request;
using WordHunt.DataInterfaces.Words.Result;

namespace WordHunt.DataInterfaces.Words
{
    public interface IWordUpdater
    {
        Task<WordUpdateResult> UpdateWord(WordUpdateRequest request);
    }
}
