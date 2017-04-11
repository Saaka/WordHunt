using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class Word
    {
        [Required]
        public long Id { get; set; }
        public string Value { get; set; }

    }
}
