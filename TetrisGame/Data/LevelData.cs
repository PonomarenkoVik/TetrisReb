using System;
using TetrisAbstract.Enum;
using TetrisAbstract.GameClasses;

namespace TetrisGame.Data
{
    internal static class LevelData
    {

        private static readonly Random Rnd = new Random();
        public static float GetVelocity(int level)
        {
            return LevelVelocity[level];
        }

        public static void GetLevelFilling(GameBoardData board)
        {
            byte[,] fillLevel = FillingOfLevels[board.Level];
            if (fillLevel != null)
            {

                for (int i = fillLevel.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < TetrisInitialData.FieldWidth; j++)
                    {

                        if (fillLevel[i, j] == 1)
                        {
                            //entering the points of the figure into the Field of the structure board
                            board.Field[j, (TetrisInitialData.FieldHeight - 1) - i] =
                                (TColor)(Rnd.Next(0, TetrisInitialData.NumberOfColors));
                        }
                    }
                }
            }
        }


        #region Data for game levels

        //velocity of moving of the figure on each of ten level
        private static readonly float[] LevelVelocity = { 1, 1, 1.2f, 1.2f, 1.5f, 1.71f, 2, 2.4f, 3, 6 };


        private static readonly byte[][,] FillingOfLevels ={
            null,                                 //Level 1
            null,                                 //Level 2
            null,                                 //Level 3
            new byte[,] {{1,1,0,1,1,0,0,1,1,1}},  //Level 4
 
            new byte[,] {
                {1,1,0,1,1,0,0,1,1,1},   //Level 5
                {0,1,1,1,0,1,1,1,0,1}},

            new byte[,] {
                {1,1,1,1,1,0,0,1,1,1},   //Level 6
                {0,1,0,1,0,1,1,1,1,1},
                {0,1,0,1,1,1,0,0,1,0}},
 
            new byte[,] {
                {1,1,0,1,1,0,0,1,1,1},   //Level 7
                {0,1,0,1,0,1,0,0,0,1},
                {0,1,0,1,0,1,1,1,1,1}},

            new byte[,] {
                {1,1,0,1,1,0,0,1,1,1},   //Level 8
                {1,1,1,0,1,0,1,1,0,1},
                {1,1,0,0,1,0,1,0,1,1},
                {1,0,1,1,0,0,0,1,1,1}},
 
            new byte[,] {
                {1,1,0,1,1,0,1,1,1,1},   //Level 9
                {0,1,1,1,1,0,1,0,1,0},
                {0,1,0,0,1,0,1,0,1,1},
                {1,1,1,1,1,0,1,0,0,1}},
 
            new byte[,] {
                {1,0,0,1,1,0,1,1,1,1},   //Level 10
                {0,0,1,1,1,0,1,0,1,0},
                {0,0,1,0,1,0,1,0,1,1},
                {1,0,1,1,1,0,1,0,0,1}} 
        };

        #endregion
    }
}
