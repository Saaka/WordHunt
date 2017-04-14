using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Categories.DTO;
using WordHunt.DataInterfaces.Common;

namespace WordHunt.DataInterfaces.Categories.Result
{
    public class CategoryGetRequest : RequestResult
    {
        public CategoryGetRequest() : base() { }
        public CategoryGetRequest(string error) : base(error) { }

        public Category Category { get; set; }
    }
}
