using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Classes;
using TetrisGame.Classes;
using FigureData = TetrisAbstract.FigureData;

namespace TetrisGame.Extensions
{
    public static class FigureExtension
    {
        public static FigureData ToFigureData(this Figure fig, bool isCurrent)
        {
            FigureData figure = new FigureData();
            figure.Type = fig.FigureType;
            figure.Color = fig.Color;
            figure.Body = fig.Body;
            figure.IsCurrent = isCurrent;
            return figure;
        }

        public static Figure ToFigureData(this FigureData fig)
        {
            string name = FigureInitialData.FigureNames[(int) fig.Type];
            bool isTurnable = FigureInitialData.FigureTurnability[(int) fig.Type];
            Figure figure = new TetrisFigure(name, fig.Type, isTurnable, fig.Color, fig.Body);          
            return figure;
        }
    }
}
