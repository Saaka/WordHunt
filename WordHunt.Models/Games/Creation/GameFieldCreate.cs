using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Base.Enums.Game;

namespace WordHunt.Models.Games.Creation
{
    public class GameFieldCreate
    {
        public int GameId { get; set; }
        public string Word { get; set; }
        public FieldType FieldType { get; set; }
        public int? TeamId { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
    }
}
