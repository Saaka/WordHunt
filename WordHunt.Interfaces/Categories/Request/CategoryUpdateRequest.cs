using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Interfaces.Categories.Request
{
    public class CategoryUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
