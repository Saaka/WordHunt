using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual List<Word> Words { get; set; }
    }
}
