
namespace TetrisGame.Data
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
