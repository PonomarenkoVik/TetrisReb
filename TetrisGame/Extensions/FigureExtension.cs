using TetrisAbstract.GameClasses;
using TetrisGame.Classes;
using TetrisGame.Data;
using FigureData = TetrisAbstract.GameClasses.FigureData;

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
            bool isTurnable = FigureInitialData.FigureTurnability[(int) (fig.Type - 1)];
            Figure figure = new TetrisFigure(fig.Type, isTurnable, fig.Color, fig.Body);          
            return figure;
        }
    }
}
