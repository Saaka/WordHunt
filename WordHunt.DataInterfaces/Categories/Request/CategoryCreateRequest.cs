﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Interfaces.Categories.Request
{
    public class CategoryCreateRequest
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}
