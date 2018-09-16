using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract
{
    public class GameBoardData
    {
        public int IdSavePoint { get; set; }
        public int IdField { get; set; }
        public DateTime Date { get; set; }
        public int Level { get; set; }
        public int BurnedLine { get; set; }
        public int Score { get; set; }
        public TColor[,] Field { get; set; }
    }
}
