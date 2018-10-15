using System.Collections.Generic;
using System.Linq;
using TetrisAbstract.Enum;
using TetrisAbstract.GameClasses;

namespace TetrisDataLayer.Extensions
{
    public static class FigureExtension
    {

        public static FigureData ToTetrisFigure(this Figure source)
        {
            FigureData fData = new FigureData();
            byte[,] body = new byte[FigureData.FigurePoints, 2];
            List<Point> bodyPoints = source.Points.ToList();
            int index = 0;
            foreach (var point in bodyPoints)
            {
                body[index, 0] = point.X;
                body[index, 1] = point.Y;
                index++;
            }

            fData.Body = body;
            fData.Color = (TColor)bodyPoints[0].IdColorP;
            int? type = source.IdFigureType;
            if (type != null)
            {
                fData.Type = (FigureTypes)type;
            }
            return fData;
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
