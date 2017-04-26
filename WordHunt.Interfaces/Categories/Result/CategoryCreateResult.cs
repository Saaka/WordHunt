using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Categories.DTO;
using WordHunt.Interfaces.Common;

namespace WordHunt.Interfaces.Categories.Result
{
    public class CategoryCreateResult : RequestResult
    {
        public CategoryCreateResult() : base() { }
        public CategoryCreateResult(string error) : base(error) { }

        public Category Category { get; set; }
    }
}
