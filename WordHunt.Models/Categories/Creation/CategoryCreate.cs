using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Models.Categories.Creation
{
    public class CategoryCreate
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }

    public class CategoryCreateResult
    { 
        public int CategoryId { get; set; }
    }
}
