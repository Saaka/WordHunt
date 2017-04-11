using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class Word
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string Value { get; set; }
        public long LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public long? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
