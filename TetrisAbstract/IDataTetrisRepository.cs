using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract
{
    public interface IDataTetrisRepository
    {
        bool Save(TetrisSavePoint point, TColor[,] field, TetrisFigure currentFigure, TetrisFigure nextFigure);
        List<TetrisSavePoint> GetSavePoints();
        BoardPoint[,] GetField(int idField);
        TetrisFigure GetCurrentFigure(int idSavePoint);
        TetrisFigure GetNextFigure(int idSavePoint);
    }
}
