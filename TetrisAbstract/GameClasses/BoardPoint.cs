using TetrisAbstract.Enum;

namespace TetrisAbstract.GameClasses
{
    public class BoardPoint
    {
        public BoardPoint(TColor color)
        {
            Col = color;
        }
        public TColor Col { get; set; } // point color (from 0  to Figure.NumberOfColors)
    }
}
