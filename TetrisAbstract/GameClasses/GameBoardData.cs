using System;
using TetrisAbstract.Enum;

namespace TetrisAbstract.GameClasses
{
    public class GameBoardData : ICloneable
    {
        public int IdSavePoint { get; set; }
        public int IdField { get; set; }
        public DateTime Date { get; set; }
        public int Level { get; set; }
        public int BurnedLine { get; set; }
        public int Score { get; set; }
        public TColor[,] Field { get; set; }
        public object Clone()
        {
            GameBoardData board = (GameBoardData)MemberwiseClone();
            board.Field = (TColor[,])Field.Clone();
            return board;
        }
    }
}
