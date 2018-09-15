using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract;
using TetrisAbstract.Enum;

namespace TetrisDataLayer
{
    public static class Adapter
    {
        public static TetrisSavePoint ToSavePoint(this SavePoint savePoint)
        {
            TetrisSavePoint tetrisSavePoint = new TetrisSavePoint();
            tetrisSavePoint.IdSavePoint = savePoint.IdSavePoint;
            if (savePoint.IdField != null)
            {
                tetrisSavePoint.IdField = (int) savePoint.IdField;
            }
            tetrisSavePoint.Level = savePoint.Level;
            tetrisSavePoint.BurnedLine = savePoint.BurnedLine;
            tetrisSavePoint.Score = savePoint.Score;
            tetrisSavePoint.Date = savePoint.Date;
            return tetrisSavePoint;
        }

        public static List<TetrisSavePoint> ToSavePoints(this List<SavePoint> savePoints)
        {
            List<TetrisSavePoint> tetrisSavePoints = new List<TetrisSavePoint>();
            foreach (var savePoint in savePoints)
            {
                tetrisSavePoints.Add(savePoint.ToSavePoint());
            }
            return tetrisSavePoints;
        }

        public static BoardPoint[,] ToTetrisField(this List<Point> points)
        {
            BoardPoint[,] field = new BoardPoint[GameData.FieldWidth, GameData.FieldHeight];          
            foreach (var point in points)
            {
                int x = point.X;
                int y = point.Y;
                TColor color = (TColor)point.IdColorP;
                BoardPoint boardPoint= new BoardPoint(color);
                field[x, y] = boardPoint;
            }
            return field;
        }

        public static TetrisFigure ToTetrisFigure(this Figure sourceFigure)
        {
            TetrisFigure figure = new TetrisFigure();
            var points = sourceFigure.Points;
            int[,] body = new int[2, GameData.FigureHeight];
            List<Point> bodyPoints = sourceFigure.Points.ToList();
            int index = 0;
            foreach (var point in bodyPoints)
            {
                body[0, index] = point.X;
                body[1, index] = point.Y;
                index++;
            }
            figure.Color = (TColor)bodyPoints[0].Colors.IdColorP;
            var type = sourceFigure.IdFigureType;
            figure.Type = (FiguresTypes)type;
            return figure;
        }

        public static SavePoint ToSavePoint(this TetrisSavePoint savePoint)
        {
            SavePoint saveP = new SavePoint();
            saveP.BurnedLine = savePoint.BurnedLine;
            saveP.Date = savePoint.Date;
            saveP.Level = savePoint.Level;
            saveP.Score = savePoint.Score;
            return saveP;
        }

        public static Figure ToFigure(this TetrisFigure sFigure)
        {
            Figure fig = new Figure();
            fig.CurrentFig = sFigure.IsCurrent;
            fig.IdFigureType = (int)sFigure.Type;
            fig.IdSavePoint = sFigure.IdSavePoint;
            return fig;
        }

    }
}
