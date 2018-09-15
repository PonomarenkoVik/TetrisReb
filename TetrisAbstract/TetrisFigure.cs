using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract
{
    public class TetrisFigure
    {
        public TColor Color { get; set; }
        public int IdSavePoint { get; set; }
        public bool IsCurrent { get; set; }
        public int[,] Body { get; set; }
        public FiguresTypes Type { get; set; }
    }
}
