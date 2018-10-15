using TetrisAbstract.Enum;
using TetrisAbstract.GameClasses;

namespace TetrisGame.Classes
{
    internal class TetrisFigure : Figure
    {
        public TetrisFigure(FigureTypes type, bool isRotatable, TColor color, byte[,] body) : base(type, isRotatable, color, body)
        {
        }


    }
}
