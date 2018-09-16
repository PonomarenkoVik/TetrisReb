using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;

namespace TetrisGame.Classes
{
    internal class FigureCreator
    {
        private static readonly Random Rnd = new Random();
        public Figure GetNewFigure()
        {                   
            int figureType = Rnd.Next(0, FigureInitialData.FigureNames.Length);
            byte color = (byte)Rnd.Next(0, TetrisInitialData.NumberOfColors);
            Figure figure = new TetrisFigure(FigureInitialData.FigureNames[figureType], (FigureTypes)figureType, FigureInitialData.FigureTurnability[figureType], (TColor)color, (byte[,])FigureInitialData.BodyFigures[figureType].Clone());

            return figure;
        }

    }
}
