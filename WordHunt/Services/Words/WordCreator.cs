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

        public WordCreator(IAppDbContext context,
            IWordCreatorValidator validator,
            IWordMapper mapper)
        {
            this.context = context;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<WordCreateResult> CreateWord(WordCreate model)
        {
            await validator.ValidateCreateModel(model);

            var newWord = mapper.MapWord(model);
            await context.Words.AddAsync(newWord);
            await context.SaveChangesAsync();

            return new WordCreateResult()
            {
                WordId = newWord.Id
            };
        }
    }
}
