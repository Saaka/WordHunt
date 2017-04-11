using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordHunt.Data.Entities
{
    public class Language
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
