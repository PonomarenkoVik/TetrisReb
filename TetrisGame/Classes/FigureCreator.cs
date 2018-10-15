using System;
using TetrisAbstract.Enum;
using TetrisAbstract.GameClasses;
using TetrisGame.Data;

namespace TetrisGame.Classes
{
    internal class FigureCreator
    {
        private static readonly Random Rnd = new Random();
        public Figure GetNewFigure()
        {                   
            int figureType = Rnd.Next(1, FigureInitialData.BodyFigures.Length + 1);
            byte color = (byte)Rnd.Next(1, TetrisInitialData.NumberOfColors + 1);
            Figure figure = new TetrisFigure((FigureTypes)figureType, FigureInitialData.FigureTurnability[figureType - 1], (TColor)color, (byte[,])FigureInitialData.BodyFigures[figureType - 1].Clone());
            return figure;
        }

    }
}
