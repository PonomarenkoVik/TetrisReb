using System;
using System.Collections.Generic;
using System.Drawing;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;

namespace TetrisLogic
{
    class FigureMover
    {

        public event Action GmOverEvent;
        public event Action ExchFigEvent;
        public event Action LvlUpEvent;
        public event EventHandler<ActionEventArgs> StepEvent;
        public event Action<GameBoardData> BurnLine;


        public void Move(GameBoardData gameBoard, ref Figure currentFigure, Direction dir)
        {
            if (dir == Direction.Down)
            {
                if (FigureMove(dir, currentFigure, gameBoard))
                {
                    if (StepEvent != null)
                    {
                        StepEvent(this, new ActionEventArgs(TypeAction.Step, dir));
                    }
                }
                else
                {
                    CopyCurrentFigureToField(currentFigure, gameBoard.Field);

                    if (BurnLine != null)
                    {
                        BurnLine(gameBoard);
                    }
                    if (CheckScoreLevelUp(gameBoard))
                    {
                        if (LvlUpEvent != null)
                        {
                            LvlUpEvent();
                        }
                    }

                    if (ExchFigEvent != null)
                    {
                        ExchFigEvent();
                    }
                    if (!CheckFreeArea(currentFigure, gameBoard.Field))
                    {
                        currentFigure = null;

                        if (GmOverEvent != null)
                        {
                            GmOverEvent();
                        }
                    }
                }
            }
            else
            {
                if (FigureMove(dir, currentFigure, gameBoard))
                {
                    if (StepEvent != null)
                    {
                        StepEvent(this, new ActionEventArgs(TypeAction.Step, dir));
                    }
                }
            }
        }

        private bool CheckFreeArea(Figure currFig, TColor[,] gameBoardField)
        {
            bool result = true;
            byte[,] body = currFig.Body;
            for (int i = 0; i < body.GetLength(0); i++)
            {
                byte x = body[i, 0];
                byte y = body[i, 1];
                if (gameBoardField[x, y] != TColor.Empty)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private bool CheckScoreLevelUp(GameBoardData gameBoard)
        {
            return (gameBoard.Score - gameBoard.Level * TetrisInitialData.LimitScore >= TetrisInitialData.LimitScore + gameBoard.Level * TetrisInitialData.LimitScore) && gameBoard.Level < 9;
        }

        private bool FigureMove(Direction dir, Figure currentFigure, GameBoardData gameBoard)
        {
            bool result = false;
            int dx;
            int dy;
            // correcting delta
            GetDeltaByDirection(dir, out  dx, out dy);

            if (CheckPermissionToMove(dir, currentFigure, gameBoard))
            {
                for (int i = 0; i < currentFigure.Body.GetLength(0); i++)
                {
                    currentFigure.Body[i, 0] = (byte)(currentFigure.Body[i, 0] + dx);
                    currentFigure.Body[i, 1] = (byte)(currentFigure.Body[i, 1] + dy);
                }
                result = true;
            }
            return result;
        }

        private bool CheckPermissionToMove(Direction dir, Figure currentFigure, GameBoardData gameBoard)
        {
            bool result = true;

            int dx;
            int dy;
            List<Point> points = GetBoundaryFigurePoints(currentFigure.Body, dir);
            GetDeltaByDirection(dir, out dx, out dy);
            foreach (var point in points)
            {

                int x = point.X + dx;
                int y = point.Y + dy;
                if (!CheckAllowedRegion(dir, x, y) || gameBoard.Field[x, y] != TColor.Empty)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool CheckAllowedRegion(Direction dir, int x, int y)
        {
            return x >= 0 && y >= 0 && x < TetrisInitialData.FieldWidth && y < TetrisInitialData.FieldHeight;
        }

        private List<Point> GetBoundaryFigurePoints(byte[,] body, Direction dir)
        {
            List<Point> points = new List<Point>();

            Func<int, int, List<Point>, bool> condition1 = null;
            Func<int, int, List<Point>, bool> condition2 = null;


            switch (dir)
            {
                case Direction.Down:
                    condition1 = (k, index, p) => (k == p[index].X);
                    condition2 = (k, index, p) => (k > p[index].Y);
                    break;
                case Direction.Left:
                    condition1 = (k, index, p) => (k == p[index].Y);
                    condition2 = (k, index, p) => (k < p[index].X);
                    break;
                case Direction.Right:
                    condition1 = (k, index, p) => (k == p[index].Y);
                    condition2 = (k, index, p) => (k > p[index].X);
                    break;
            }

            for (int i = 0; i < body.GetLength(0); i++)
            {
                int x = body[i, 0];
                int y = body[i, 1];
                int k1;
                int k2;
                if (dir == Direction.Down)
                {
                    k1 = x;
                    k2 = y;
                }
                else
                {
                    k1 = y;
                    k2 = x;
                }
                if (points.Count == 0)
                {
                    points.Add(new Point(x, y));
                }
                else
                {
                    bool findFitColumn = false;
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (condition1 != null && condition1(k1, j, points))
                        {
                            findFitColumn = true;
                            if (condition2(k2, j, points))
                            {
                                points[j] = new Point(x, y);
                            }
                        }
                    }

                    if (!findFitColumn)
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }
            return points;
        }

        private void GetDeltaByDirection(Direction dir, out int dx, out int dy)
        {
            dx = 0;
            dy = 0;
            switch (dir)
            {
                case Direction.Down:
                    dy = 1;
                    break;
                case Direction.Left:
                    dx = -1;
                    break;
                case Direction.Right:
                    dx = 1;
                    break;
            }
        }

        private bool CopyCurrentFigureToField(Figure currentF, TColor[,] field)
        {
            bool result = true;
            byte[,] body = currentF.Body;
            for (int i = 0; i < body.GetLength(0); i++)
            {
                int x = body[i, 0];
                int y = body[i, 1];
                if (field[x, y] == TColor.Empty)
                {
                    field[x, y] = currentF.Color;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}