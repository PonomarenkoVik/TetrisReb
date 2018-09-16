using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract
{
    public class FigureData
    {
        public const int HeightWidth = 4;
        public const int FigurePoints = 4;

        public TColor Color { get; set; }
        public int IdSavePoint { get; set; }
        public bool IsCurrent { get; set; }
        public byte[,] Body { get; set; }
        public FigureTypes Type { get; set; }
    }
}
