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
        public int Id { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
