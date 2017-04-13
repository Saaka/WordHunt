using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Words.DTO
{
    public class Word
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
        public long? CategoryId { get; set; }
    }
}
