﻿using System.Threading.Tasks;
using WordHunt.DataInterfaces.Words.Request;
using WordHunt.DataInterfaces.Words.Result;

namespace WordHunt.DataInterfaces.Words
{
    public interface IWordCreator
    {
        Task<CreateWordResult> CreateWord(WordCreateRequest wordCreate);
    }
}
