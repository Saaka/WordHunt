using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.DataInterfaces.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameField
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public string Word { get; set; }
        public FieldType Type { get; set; }
        public long? TeamId { get; set; }
        public bool Checked { get; set; }
        public long? CheckedByTeamId { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
    }
}
