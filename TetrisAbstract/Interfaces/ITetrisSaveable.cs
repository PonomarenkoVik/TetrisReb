using System.Collections.Generic;
using TetrisAbstract.GameClasses;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisSaveable
    {
        void Save();
        List<GameBoardData> GetSavePoints();
        void OpenGame(int idSavePoint);
    }
}
