using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class Language
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual List<Word> Words { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
