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

        public static FigureData ToTetrisFigure(this Figure source)
        {
            FigureData fData = new FigureData();
            int[,] body = new int[2, FigureData.FigurePoints];
            List<Point> bodyPoints = source.Points.ToList();
            int index = 0;
            foreach (var point in bodyPoints)
            {
                body[0, index] = point.X;
                body[1, index] = point.Y;
                index++;
            }
            fData.Color = (TColor)bodyPoints[0].Colors.IdColorP;
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
