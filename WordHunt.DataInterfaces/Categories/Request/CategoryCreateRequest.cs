﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Categories.Request
{
    public class CategoryCreateRequest
    {
        public long LanguageId { get; set; }
        public string Name { get; set; }
    }
}