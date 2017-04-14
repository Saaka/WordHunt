using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Languages.Results;

namespace WordHunt.DataInterfaces.Languages
{
    public interface ILanguageProvider
    {
        Task<LanguageListGetResult> GetLanguageList();
    }
}
