﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Words.Request
{
    public class WordListGetRequest
    {
        public long LanguageId { get; set; }
        public long? CategoryId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public bool OrderByDesc { get; set; }
        public string Value { get; set; }
    }
}