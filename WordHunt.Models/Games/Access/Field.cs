using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Models.Games.Access
{
    public class Field
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public bool Checked { get; set; }
        public bool CheckedByRightTeam { get; set; }
        public int? CheckedByTeamId { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
    }
}
