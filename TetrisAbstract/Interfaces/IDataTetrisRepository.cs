using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract
{
    public interface IDataTetrisRepository : IDisposable
    {
        void Save(GameBoardData point, FigureData currentFigureData, FigureData nextFigureData);
        List<GameBoardData> GetSavePoints();
        GameBoardData GetGameBoard(int idSavePoint);
        FigureData GetFigure(int idSavePoint, bool isCurrent);
    }
}
