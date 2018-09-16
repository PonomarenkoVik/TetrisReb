using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisSaveable
    {
        void Save();
        List<GameBoardData> GetSavePoints();
        void OpenGame(int idSavePoint);
    }
}
