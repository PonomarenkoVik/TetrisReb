using System.Collections.Generic;
using TetrisAbstract.GameClasses;

namespace TetrisAbstract.Interfaces
{
    public interface IDataTetrisRepository
    {
        void Save(GameBoardData point, FigureData currentFigure, FigureData nextFigureData);
        List<GameBoardData> GetSavePoints();
        GameBoardData GetGameBoard(int idSavePoint);
        FigureData GetFigure(int idSavePoint, bool isCurrent);
    }
}
