using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Words.DTO.Access
{
    public class Word
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public long? CategoryId { get; set; }
    }
}
