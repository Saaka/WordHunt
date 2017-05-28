using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Events
{
    public class FieldChecked
    {
        public int GameId { get; set; }
        public int FieldId { get; set; }
        public bool Checked { get; set; }
        public FieldType Type { get; set; }
        public int TeamId { get; set; }
    }
}
