﻿using System.Threading.Tasks;
using WordHunt.DataInterfaces.Words.Request;
using WordHunt.DataInterfaces.Words.Result;

namespace WordHunt.DataInterfaces.Words
{
    public interface IWordProvider
    {
        Task<GetWordListResult> GetWordList(WordListRequest request);
        Task<GetWordResult> GetWord(long wordId);
    }
}
