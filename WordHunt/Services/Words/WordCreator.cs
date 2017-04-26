using System;
using System.Threading.Tasks;
using WordHunt.Services.Words.Mapper;
using WordHunt.Data;
using WordHunt.Models.Words.Creation;

namespace WordHunt.Services.Words
{
    public interface IWordCreator
    {
        Task<WordCreateResult> CreateWord(WordCreate wordCreate);
    }

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

        public async Task<WordCreateResult> CreateWord(WordCreate model)
        {
            await validator.ValidateRequest(model);

            var newWord = mapper.MapWord(model);
            await context.Words.AddAsync(newWord);
            await context.SaveChangesAsync();

            var wordModel = await wordProvider.GetWord(newWord.Id);

            return new WordCreateResult()
            {
                Word = wordModel
            };
        }
    }
}
