using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract
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
