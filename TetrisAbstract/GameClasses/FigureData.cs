using System;
using TetrisAbstract.Enum;

namespace TetrisAbstract.GameClasses
{
    public class FigureData : ICloneable
    {
        public const int HeightWidth = 4;
        public const int FigurePoints = 4;

        public TColor Color { get; set; }
        public int IdSavePoint { get; set; }
        public bool IsCurrent { get; set; }
        public byte[,] Body { get; set; }
        public FigureTypes Type { get; set; }
        public object Clone()
        {
            FigureData fig = (FigureData)MemberwiseClone();
            fig.Body = (byte[,])Body.Clone();
            return fig;
        }
    }
}
