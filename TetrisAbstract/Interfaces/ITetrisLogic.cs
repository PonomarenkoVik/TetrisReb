using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisLogic
    {
        event Action ExchangeFigureEvent;
        event Action LevelUpEvent;
        event Action GOverEvent;
        event EventHandler<SoundEventArgs> SndEvent;
        void DeleteEmptyPoint(Figure currentFigure);
        void Turn(GameBoardData gameBoard, Figure figure);
        void Move(GameBoardData gameBoard, Figure currentFigure, Direction dir);
    }
}
