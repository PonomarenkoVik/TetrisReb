using System;
using System.Collections.Generic;
using System.Linq;
using TetrisAbstract;
using TetrisAbstract.Enum;
using TetrisDataLayer.Extensions;

namespace TetrisDataLayer
{
    public class TetrisRepository : TetrisDataContext, IDataTetrisRepository
    {

        #region Save section

        public void Save(GameBoardData point, FigureData currentFigureData, FigureData nextFigureData)
        {           
            int idSavePoint = AddSavePoint(point);
            AddField(point.Field, idSavePoint);
            AddFigure(currentFigureData, idSavePoint, true);
            AddFigure(nextFigureData, idSavePoint, false);          
        }

        private int AddSavePoint(GameBoardData gameBoardData)
        {
            var sp = SavePoints.Add(gameBoardData.ToSavePoint());
            SaveChanges();
            return sp.IdSavePoint;
        }

        private void AddField(TColor[,] field, int savePoint)
        {
            var f = new Field
            {
                IdSavePoint = savePoint
            };
            Fields.Add(f);
            SaveChanges();
            AddFieldPoints(field, f.IdField);
        }

        private void AddFieldPoints(TColor[,] field, int idField)
        {
            for (byte i = 0; i < field.GetLength(1); i++)
            {
                for (byte j = 0; j < field.GetLength(0); j++)
                {
                    Point p = new Point
                    {
                        IdField = idField,
                        IdColorP = (int) field[j, i],
                        X = j,
                        Y = i
                    };
                    Points.Add(p);
                }
            }
            SaveChanges(); // ????????????
        }


        private void AddFigure(FigureData figureData, int idSavePoint, bool isCurrent)
        {
            Figure fig = figureData.ToFigure();
            fig.IdSavePoint = idSavePoint;
            fig.CurrentFig = isCurrent;
            Figures.Add(fig);
            SaveChanges();
            AddFigurePoints(figureData, fig.IdFig);
        }

        private void AddFigurePoints(FigureData figureData, int idFigure)
        {
            byte[,] body = figureData.Body;
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                byte x = (byte)body[0, i];
                byte y = (byte)body[1, i];
                Point point = new Point
                {
                    X = x,
                    Y = y,
                    IdColorP = (int) figureData.Color,
                    IdFig = idFigure
                };
                Points.Add(point);
            }
            SaveChanges();
        }

        #endregion




        #region Read section


        public List<GameBoardData> GetSavePoints()
        {
            return SavePoints.ToList().ToSavePoints();
        }

        public GameBoardData GetGameBoard(int idSavePoint)
        {
            SavePoint savePoint = (from point in SavePoints
                where point.IdSavePoint == idSavePoint
                select point).First();
            int? idField = savePoint.IdField;
            GameBoardData boardData = savePoint.ToTetrisGameBoard();
            boardData.Field = GetField(idField);
            return boardData;
        }

        public TColor[,] GetField(int? idField)
        {
            var points = from p in Points
                where p.IdField == idField
                select p;
            return points.ToList().ToTetrisField();
        }

        public FigureData GetFigure(int idSavePoint, bool isCurrent)
        {
            var fig = (from f in Figures
                where f.IdSavePoint == idSavePoint
                where f.CurrentFig == isCurrent
                select f).First().ToTetrisFigure(); 
            return fig;
        }  

        #endregion

        
    }
}
