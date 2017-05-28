using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Games.Access
{
    public class CurrentFieldState
    {
        public int TeamId { get; set; }
        public bool Checked { get; set; }
        public int? CheckedByTeamId { get; set; }
        public FieldType Type { get; set; }
    }
}
