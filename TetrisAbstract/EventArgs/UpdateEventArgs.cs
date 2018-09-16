using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract.EventArgs
{
    public class UpdateEventArgs : System.EventArgs
    {

        public UpdateEventArgs(GameBoardData boardData, FigureData currFig, FigureData nextFig, float velocity)
        {
            GameBoardData = boardData;
            CurrentFigureData = currFig;
            NextFigureData = nextFig;
            Velocity = velocity;
        }
        public GameBoardData GameBoardData { get; private set; }
        public FigureData CurrentFigureData { get; private set; }
        public FigureData NextFigureData { get; private set; }
        public float Velocity { get; private set; }
    }
}
