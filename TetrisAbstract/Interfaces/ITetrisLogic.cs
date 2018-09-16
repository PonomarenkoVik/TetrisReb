using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisLogic
    {
        void DeleteEmptyPoint(Figure currentFigure);
        void Turn(GameBoardData gameBoard, Figure currentFigure);
        void Move(GameBoardData gameBoard, Figure currentFigure, Direction dir);

        event Action ExchangeFigureEvent;
        event Action LevelUpEvent;
        event Action GameOverEvent;
    }
}
