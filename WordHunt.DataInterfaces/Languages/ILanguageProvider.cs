using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Languages.Results;

namespace WordHunt.Interfaces.Languages
{
    public interface ILanguageProvider
    {
        Task<LanguageListGetResult> GetLanguageList();
    }
}
