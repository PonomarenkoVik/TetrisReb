using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract.Classes
{
    public abstract class Figure : ICloneable
    {

        protected Figure(string name, FigureTypes type, bool isRotatable, TColor color, byte[,] body)
        {
            Color = color;
            _body = (byte[,])body.Clone();
            IsRotatable = isRotatable;
            Name = name;
            FigureType = type;
        }
        public virtual string Name { get; private set; }

        public virtual TColor Color { get; private set; }

        public virtual byte[,] Body
        {
            get
            {
                return (byte[,])_body.Clone();
            }
            set
            {
                _body = (byte[,])value.Clone();
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
