using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WordHunt.Services.Words.Mapper;
using WordHunt.Data;
using WordHunt.Models.Words.Modification;

namespace WordHunt.Services.Words
{
    public interface IWordUpdater
    {
        Task<WordUpdateResult> UpdateWord(WordUpdate model);
    }

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

        public async Task<WordUpdateResult> UpdateWord(WordUpdate model)
        {
            await validator.ValidateRequest(model);

            var toUpdate = await context.Words.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (toUpdate == null)
                throw new ArgumentException($"Can't fine word with id {model.Id}");

            toUpdate = mapper.MapWord(toUpdate, model);

            await context.SaveChangesAsync();

            var wordModel = await wordProvider.GetWord(model.Id);

            return new WordUpdateResult()
            {
                Word = wordModel
            };
        }
    }
}
