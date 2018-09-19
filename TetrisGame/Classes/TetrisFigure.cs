using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;

namespace TetrisGame.Classes
{
    internal class TetrisFigure : Figure
    {
        public TetrisFigure(string name, FigureTypes type, bool isRotatable, TColor color, byte[,] body) : base(name, type, isRotatable, color, body)
        {
        }


    }
}
