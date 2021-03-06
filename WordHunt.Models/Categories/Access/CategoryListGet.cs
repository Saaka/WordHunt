﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Models.Categories.Access
{
    public class CategoryListGet
    {
        public int LanguageId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public bool OrderByDesc { get; set; }
        public string Name { get; set; }
    }

    public class CategoryListGetResult 
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<CategoryModel> CategoryList { get; set; }
    }
}
