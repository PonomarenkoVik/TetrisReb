using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract;
using TetrisAbstract.Enum;

namespace TetrisDataLayer
{
    public class TetrisRepository : TetrisDataContext, IDataTetrisRepository
    {
        public bool Save(TetrisSavePoint point, TColor[,] field, TetrisFigure currentFigure, TetrisFigure nextFigure)
        {
            throw new NotImplementedException();
        }


        private int AddSavePoint(TetrisSavePoint savePoint)
        {
            SavePoint savP = new SavePoint();
            var sp = SavePoints.Add(savePoint.ToSavePoint());
            SaveChanges();
            return sp.IdSavePoint;
        }

        private int AddField(TColor[,] fPoints, int savePoint)
        {
            Field f = new Field();
            f.IdSavePoint = savePoint;
            var field = Fields.Add(f);
            SaveChanges();
            return field.IdField;
        }

        private void AddPoints(TColor[,] fPoints, int idField)
        {
            for (byte i = 0; i < fPoints.GetLength(1); i++)
            {
                for (byte j = 0; j < fPoints.GetLength(0); j++)
                {
                    Point p = new Point();
                    p.IdField = idField;
                    p.IdColorP = (int)fPoints[j, i];
                    p.X = j;
                    p.Y = i;
                    Points.Add(p);
                }               
            }
            SaveChanges();
        }

        private void AddFigure(TetrisFigure fig, int savePoint)
        {
           Figure fig = fig.
        }






        public List<TetrisSavePoint> GetSavePoints()
        {
            return SavePoints.ToList().ToSavePoints();
        }

        public BoardPoint[,] GetField(int idField)
        {
            var points = from p in Points
                where p.IdField == idField
                select p;
            return points.ToList().ToTetrisField();
        }

        public TetrisFigure GetCurrentFigure(int idSavePoint)
        {
            var qFigure = (from f in Figures
                         where f.IdSavePoint == idSavePoint
                         where f.CurrentFig == true
                         select f).ToList();
           
            TetrisFigure fig;
            if (qFigure.Count == 1)
	        {
		       fig = qFigure[0].ToTetrisFigure();  
	        }
            else
            {
                throw new NotImplementedException();
            }
            return fig;
        }

        public TetrisFigure GetNextFigure(int idSavePoint)
        {
            var qFigure = (from f in Figures
                           where f.IdSavePoint == idSavePoint
                           where f.CurrentFig != true
                           select f).ToList();

            TetrisFigure fig;
            if (qFigure.Count == 1)
            {
                fig = qFigure[0].ToTetrisFigure();
            }
            else
            {
                throw new NotImplementedException();
            }
            return fig;
        }
    }
}
