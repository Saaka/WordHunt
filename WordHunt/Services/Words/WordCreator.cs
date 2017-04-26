using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Services.Words.Mapper;
using WordHunt.Interfaces.Words;
using WordHunt.Interfaces.Words.Request;
using WordHunt.Interfaces.Words.Result;
using WordHunt.Data;

namespace WordHunt.Services.Words
{
    public class WordCreator : IWordCreator
    {
        private readonly IAppDbContext context;
        private readonly IWordCreatorValidator validator;
        private readonly IWordMapper mapper;
        private readonly IWordProvider wordProvider;

        public WordCreator(IAppDbContext context,
            IWordCreatorValidator validator,
            IWordMapper mapper,
            IWordProvider wordProvider)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
            this.wordProvider = wordProvider;
        }

        public async Task<WordCreateResult> CreateWord(WordCreateRequest createRequest)
        {
            try
            {
                var validatorResult = await validator.ValidateRequest(createRequest);
                if (!validatorResult.IsSuccess)
                    return new WordCreateResult(validatorResult.Error);

                var newWord = mapper.MapCreateRequest(createRequest);
                await context.Words.AddAsync(newWord);
                await context.SaveChangesAsync();

                var getWordResult = await wordProvider.GetWord(newWord.Id);

                if (!getWordResult.IsSuccess)
                    return new WordCreateResult(getWordResult.Error);

                return new WordCreateResult()
                {
                    Word = getWordResult.Word
                };                
            }
            catch(Exception ex)
            {
                return new WordCreateResult(ex.Message);
            }
        }
    }
}
