using TetrisAbstract.Enum;

namespace TetrisAbstract.EventArgs
{
    public class ActionEventArgs : System.EventArgs
    {
        public ActionEventArgs(TypeAction action, Direction dir = Direction.Empty)
        {
            Action = action;
            Direction = dir;
        }
        public TypeAction Action { get; set; }
        public Direction Direction;
    }
}
