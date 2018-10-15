
using TetrisAbstract.GameClasses;

namespace TetrisAbstract.EventArgs
{
    public class UpdateEventArgs : System.EventArgs
    {

        public UpdateEventArgs(GameBoardData boardData, FigureData currFig, FigureData nextFig, float velocity)
        {
            GameBoardData = (GameBoardData)boardData.Clone();
            if (currFig != null)
            {
                CurrentFigureData = (FigureData)currFig.Clone();
            }
            NextFigureData = (FigureData)nextFig.Clone();
            Velocity = velocity;
        }
        public GameBoardData GameBoardData { get; private set; }
        public FigureData CurrentFigureData { get; private set; }
        public FigureData NextFigureData { get; private set; }
        public float Velocity { get; private set; }
    }
}
