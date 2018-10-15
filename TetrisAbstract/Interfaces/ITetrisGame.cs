using System;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;

namespace TetrisAbstract.Interfaces
{
    public interface ITetrisGame : ITetrisSaveable
    {
        event Action GameOverEvent;
        event EventHandler<UpdateEventArgs> UpdateDataEvent;
        event Action<ActionEventArgs> ActionEvent;

        void Start();      
        void Move(Direction dir);
        void Turn();
    }
}
