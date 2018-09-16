using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;
using TetrisAbstract.Interfaces;

namespace TetrisLogic
{
    public class Logic : ITetrisLogic
    {
        public void DeleteEmptyPoint(Figure currentFigure)
        {
            int num = FigureData.HeightWidth;
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                if (currentFigure.Body[i, 1] < num)
                {
                    num = currentFigure.Body[i, 1];
                }
            }
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                currentFigure.Body[i, 1] -= (byte)num;
                currentFigure.Body[i, 0] += (FigureData.HeightWidth/2) + 1;
            }
        }

        public void Turn(GameBoardData gameBoard, Figure currentFigure)
        {
            throw new NotImplementedException();
        }

        public void Move(GameBoardData gameBoard, Figure currentFigure, Direction dir)
        {
            throw new NotImplementedException();
        }

        public event Action ExchangeFigureEvent;
        public event Action LevelUpEvent;
        public event Action GameOverEvent;
    }
}
