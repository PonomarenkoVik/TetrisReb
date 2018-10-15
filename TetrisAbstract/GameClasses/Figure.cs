using System;
using TetrisAbstract.Enum;

namespace TetrisAbstract.GameClasses
{
    public abstract class Figure : ICloneable
    {

        protected Figure(FigureTypes type, bool isRotatable, TColor color, byte[,] body)
        {
            Color = color;
            _body = (byte[,])body.Clone();
            IsRotatable = isRotatable;
            FigureType = type;
        }

        public virtual TColor Color { get; private set; }

        public virtual byte[,] Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        public bool IsRotatable { get; private set; }
        public virtual FigureTypes FigureType { get; private set; }

        public virtual object Clone()
        {
            Figure fig = (Figure)MemberwiseClone();
            fig._body = Body;
            return fig;
        }

        private byte[,] _body;
    }
}
