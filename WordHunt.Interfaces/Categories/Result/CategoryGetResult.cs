using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Categories.DTO;
using WordHunt.Interfaces.Common;

namespace WordHunt.Interfaces.Categories.Result
{
    public class CategoryGetResult : RequestResult
    {
        public CategoryGetResult() : base() { }
        public CategoryGetResult(string error) : base(error) { }

        public Category Category { get; set; }
    }
}
