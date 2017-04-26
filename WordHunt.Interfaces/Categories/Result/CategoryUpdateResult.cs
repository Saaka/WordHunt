using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Interfaces.Categories.DTO;
using WordHunt.Interfaces.Common;

namespace WordHunt.Interfaces.Categories.Result
{
    public class CategoryUpdateResult : RequestResult
    {
        public CategoryUpdateResult() : base() { }
        public CategoryUpdateResult(string error) : base(error) { }

        public Category Category{ get; set; }
    }
}