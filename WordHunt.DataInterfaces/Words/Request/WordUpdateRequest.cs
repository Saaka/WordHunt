using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Interfaces.Words.Request
{
    public class WordUpdateRequest
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? CategoryId { get; set; }
    }
}
