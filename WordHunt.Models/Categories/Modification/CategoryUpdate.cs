using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Models.Categories.Modification
{
    public class CategoryUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryUpdateResult 
    {
        public CategoryModel Category { get; set; }
    }
}
