using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Categories.DTO;
using WordHunt.DataInterfaces.Common;

namespace WordHunt.DataInterfaces.Categories.Result
{
    public class CategoryUpdateResult : RequestResult
    {
        public CategoryUpdateResult() : base() { }
        public CategoryUpdateResult(string error) : base(error) { }

        public Category Category{ get; set; }
    }
}