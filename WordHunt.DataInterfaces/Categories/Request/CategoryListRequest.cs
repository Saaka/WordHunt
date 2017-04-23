using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Categories.Request
{
    public class CategoryListRequest
    {
        public int LanguageId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public bool OrderByDesc { get; set; }
        public string Name { get; set; }
    }
}
