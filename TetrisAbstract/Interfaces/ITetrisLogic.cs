using System;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisLogic
    {
        event Action ExchangeFigureEvent;
        event Action LevelUpEvent;
        event Action GOverEvent;
        event Action<ActionEventArgs> ActionEvent;
        void DeleteEmptyPoint(Figure currentFigure);
        void Turn(GameBoardData gameBoard, Figure figure);
        void Move(GameBoardData gameBoard, ref Figure currentFigure, Direction dir);
    }
}
