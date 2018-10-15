using System.Collections.Generic;
using TetrisAbstract.Enum;
using TetrisAbstract.GameClasses;

namespace TetrisDataLayer.Extensions
{
    public static class SavePointExtension
    {
        public static GameBoardData ToTetrisGameBoard(this SavePoint savePoint)
        {
            GameBoardData gameBoardData = new GameBoardData
            {
                IdSavePoint = savePoint.IdSavePoint
            };
            if (savePoint.IdField != null)
            {
                gameBoardData.IdField = (int) savePoint.IdField;
            }
            gameBoardData.Level = savePoint.Level;
            gameBoardData.BurnedLine = savePoint.BurnedLine;
            gameBoardData.Score = savePoint.Score;
            gameBoardData.Date = savePoint.Date;
            return gameBoardData;
        }

        public static List<GameBoardData> ToSavePoints(this List<SavePoint> savePoints)
        {
            List<GameBoardData> tetrisSavePoints = new List<GameBoardData>();
            foreach (var savePoint in savePoints)
            {
                tetrisSavePoints.Add(savePoint.ToTetrisGameBoard());
            }
            return tetrisSavePoints;
        }

        public static TColor[,] ToTetrisField(this List<Point> points)
        {
            TColor[,] field = new TColor[TetrisInitialData.FieldWidth, TetrisInitialData.FieldHeight];          
            foreach (var point in points)
            {
                field[point.X, point.Y] = (TColor)point.IdColorP;
            }
            return field;
        }

        

        public static SavePoint ToSavePoint(this GameBoardData gameBoardData)
        {
            SavePoint saveP = new SavePoint
            {
                BurnedLine = gameBoardData.BurnedLine,
                Date = gameBoardData.Date,
                Level = gameBoardData.Level,
                Score = gameBoardData.Score
            };
            return saveP;
        }
    }
}
