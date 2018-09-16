using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract;
using TetrisAbstract.Enum;

namespace TetrisDataLayer.Extensions
{
    public static class FigureExtension
    {

        public static FigureData ToTetrisFigure(this Figure sourceFigure)
        {
            FigureData figureData = new FigureData();
            int[,] body = new int[2, FigureData.FigurePoints];
            List<Point> bodyPoints = sourceFigure.Points.ToList();
            int index = 0;
            foreach (var point in bodyPoints)
            {
                body[0, index] = point.X;
                body[1, index] = point.Y;
                index++;
            }
            figureData.Color = (TColor)bodyPoints[0].Colors.IdColorP;
            int? type = sourceFigure.IdFigureType;
            if (type != null)
            {
                figureData.Type = (FigureTypes)type;
            }
            return figureData;
        }


        public static Figure ToFigure(this FigureData sFigureData)
        {
            Figure fig = new Figure
            {
                CurrentFig = sFigureData.IsCurrent,
                IdFigureType = (int)sFigureData.Type,
                IdSavePoint = sFigureData.IdSavePoint
            };
            return fig;
        }
    }
}
