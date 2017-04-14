using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Categories.Request
{
    public class CategoryUpdateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
