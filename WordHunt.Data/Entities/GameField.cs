using WordHunt.Base.Enums.Game;

namespace WordHunt.Data.Entities
{
    public class GameField
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Word { get; set; }
        public FieldType Type { get; set; }
        public int? TeamId { get; set; }
        public bool Checked { get; set; }
        public int? CheckedByTeamId { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }

        public virtual Game Game { get; set; }
    }
}
