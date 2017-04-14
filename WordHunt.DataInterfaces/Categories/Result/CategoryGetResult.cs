using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Categories.DTO;
using WordHunt.DataInterfaces.Common;

namespace WordHunt.DataInterfaces.Categories.Result
{
    public class CategoryGetResult : RequestResult
    {
        public CategoryGetResult() : base() { }
        public CategoryGetResult(string error) : base(error) { }

        public Category Category { get; set; }
    }
}
