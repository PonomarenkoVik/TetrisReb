using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Classes
{
    internal static class FigureInitialData
    {


        #region Data for initializing of figures

        internal static readonly bool[] FigureTurnability =
        {
            false,  //square
            true,   //stick 
            true,   //left zigzag
            true,   //right zigzag
            true,   //right Г 
            true,   //left  Г
            true    // Т
        };

        internal static readonly string[] FigureNames =
        {
            "Square",
            "Stick",
            "Left zigzag",
            "Right zigzag",
            "Right Г",
            "Left Г",
            "T figure"

        };

        internal static readonly byte[][,] BodyFigures =
        {
            new byte[,] {{1, 1}, {2, 1}, {1, 2}, {2, 2}}, //square
            new byte[,] {{2, 0}, {2, 1}, {2, 2}, {2, 3}}, //stick 
            new byte[,] {{1, 0}, {1, 1}, {2, 1}, {2, 2}}, //left zigzag
            new byte[,] {{2, 0}, {1, 1}, {2, 1}, {1, 2}}, //right zigzag
            new byte[,] {{2, 0}, {2, 1}, {1, 2}, {2, 2}}, //right Г 
            new byte[,] {{1, 0}, {2, 0}, {2, 1}, {2, 2}}, //left  Г
            new byte[,] {{1, 1}, {2, 1}, {3, 1}, {2, 2}}, // Т
        };

        #endregion
    }
}
