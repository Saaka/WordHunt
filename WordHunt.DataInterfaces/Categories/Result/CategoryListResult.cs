using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Categories.DTO;
using WordHunt.Interfaces.Common;

namespace WordHunt.Interfaces.Categories.Result
{
    public class CategoryListResult : RequestResult
    {
        public CategoryListResult() : base() { }
        public CategoryListResult(string error) : base(error) { }

        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
    }
}
