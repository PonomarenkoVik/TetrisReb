using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisGame : ITetrisSaveable
    {
        event Action GameOverEvent;
        event EventHandler<UpdateEventArgs> UpdateDataEvent;
        event EventHandler<SoundEventArgs> SoundEvent;

        void Start();      
        void Move(Direction dir);
        void Turn();
    }
}
