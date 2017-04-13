﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Services.Words.Mapper;
using WordHunt.DataInterfaces.Words;
using WordHunt.DataInterfaces.Words.Request;
using WordHunt.DataInterfaces.Words.Result;

namespace WordHunt.Data.Services.Words
{
    public class WordCreator : IWordCreator
    {
        private readonly AppDbContext context;
        private readonly IWordCreatorValidator validator;
        private readonly IWordMapper mapper;
        private readonly IWordProvider wordProvider;

        public WordCreator(AppDbContext context,
            IWordCreatorValidator validator,
            IWordMapper mapper,
            IWordProvider wordProvider)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
            this.wordProvider = wordProvider;
        }

        public async Task<CreateWordResult> CreateWord(WordCreateRequest createRequest)
        {
            try
            {
                var validatorResult = await validator.ValidateRequest(createRequest);
                if (!validatorResult.IsSuccess)
                    return new CreateWordResult(validatorResult.ErrorMessage);

                var newWord = mapper.MapCreateRequest(createRequest);
                await context.AddAsync(newWord);
                await context.SaveChangesAsync();

                var getWordResult = await wordProvider.GetWord(newWord.Id);

                if (!getWordResult.IsSuccess)
                    return new CreateWordResult(getWordResult.Error);

                return new CreateWordResult()
                {
                    Word = getWordResult.Word
                };                
            }
            catch(Exception ex)
            {
                return new CreateWordResult(ex.Message);
            }
        }
    }
}
