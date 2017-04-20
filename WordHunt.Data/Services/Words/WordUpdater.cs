using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Services.Words.Mapper;
using WordHunt.DataInterfaces.Words;
using WordHunt.DataInterfaces.Words.Request;
using WordHunt.DataInterfaces.Words.Result;

namespace WordHunt.Data.Services.Words
{
    public class WordUpdater : IWordUpdater
    {
        private readonly IAppDbContext context;
        private readonly IWordUpdaterValidator validator;
        private readonly IWordMapper mapper;
        private readonly IWordProvider wordProvider;

        public WordUpdater(IAppDbContext context,
            IWordUpdaterValidator validator,
            IWordMapper mapper,
            IWordProvider wordProvider)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
            this.wordProvider = wordProvider;
        }

        public async  Task<WordUpdateResult> UpdateWord(WordUpdateRequest request)
        {
            try
            {
                var validatorResult = await validator.ValidateRequest(request);
                if (!validatorResult.IsSuccess)
                    return new WordUpdateResult(validatorResult.Error);

                var toUpdate = await context.Words.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (toUpdate == null)
                    return new WordUpdateResult($"Can't fine word with id {request.Id}");

                toUpdate = mapper.MapWord(toUpdate, request);

                await context.SaveChangesAsync();

                var getWordResult = await wordProvider.GetWord(request.Id);
                if (!getWordResult.IsSuccess)
                    return new WordUpdateResult(getWordResult.Error);
                
                return new WordUpdateResult()
                {
                    Word = getWordResult.Word
                };
            }
            catch(Exception ex)
            {
                return new WordUpdateResult(ex.Message);
            }
        }
    }
}
