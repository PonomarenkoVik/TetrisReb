using System;
using TetrisAbstract;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;

namespace TetrisLogic
{
    class FigureRotator
    {
        public FigureRotator()
        {
            _correction = 0;
        }
        public event EventHandler<SoundEventArgs> SoundEvent;

        public byte Correction
        {
            get
            {
                _correction = _correction == 0 ? (byte) 1 : (byte) 0;
                return _correction;
            }
        }
        public void Turn(GameBoardData gameBoard, Figure figure)
        {
            if (figure.IsRotatable && FigureTurn(gameBoard,  figure))
            {
                if (SoundEvent != null)
                {
                    SoundEvent(this, new SoundEventArgs(GameSound.Turning));
                }
            }
        }

        private bool FigureTurn(GameBoardData gameBoard, Figure figure)
        {
            bool result = true;
            int[,] turnedFigure = GetCoordTurnedFigure(figure.Body, gameBoard.Field);
            if (turnedFigure != null)
            {
                figure.Body = (byte[,])turnedFigure.Clone();
            }
            else
            {
                result = false;
            }
            return result;
        }

        private int[,] GetCoordTurnedFigure(byte[,] body, TColor[,] gameBoardField)
        {
            // determining of square of the current figure
            int xMin;
            int xMax;
            int yMax;
            int yMin;
            DeterminCoordSquareFig(body, out  xMin, out  xMax, out  yMin, out  yMax);

            // determining of of the rotation center
            byte x0 = (byte)((byte)((xMin + xMax) / 2) + Correction),
                y0 = (byte)((yMin + yMax) / 2);

            // _corr - correction factor for the figure does not move when turning left or right           

            int[,] rotatedFig = new int[FigureData.FigurePoints, 2];
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                //The reduction of the rotation center to the 0 point of coordinates
                int x = body[i, 0] - x0;
                int y = body[i, 1] - y0;

                //point rotation  around 0 by 90 degree. x1= x*cos(90) - y*sin(90), y1= x*sin(90) + y*cos(90); x1 = -y, y1 = x 

                //shift the center of rotation backwards
                int x1 = -y + x0;
                int y1 = x + y0;
                rotatedFig[i, 0] = x1;
                rotatedFig[i, 1] = y1;
            }
            if (!CheckAllowToTurn(rotatedFig, gameBoardField))
            {
                rotatedFig = null;
            }
            return rotatedFig;
        }

        private bool CheckAllowToTurn(int[,] rotatedFig, TColor[,] gameBoardField)
        {
            bool permition = true;
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                int x = rotatedFig[i, 0];
                int y = rotatedFig[i, 1];
                if (x > 9 || y > 19 || x < 0 || y < 0 || gameBoardField[x, y] != TColor.Empty)
                {
                    permition = false;
                    break;
                }
            }
            return permition;
        }

        private void DeterminCoordSquareFig(byte[,] body, out int xMin, out int xMax, out int yMin, out int yMax)
        {
            // array of coordinates of the current figure
            // determining of square of the current figure
            xMin = int.MaxValue;
            yMin = int.MaxValue;
            xMax = int.MinValue;
            yMax = int.MinValue;

            for (int i = 0; i < 4; i++)
            {
                int x = body[i, 0];
                int y = body[i, 1];
                if (x < xMin)
                {
                    xMin = x;
                }
                if (x > xMax)
                {
                    xMax = x;
                }
                if (y < yMin)
                {
                    yMin = y;
                }
                if (y > yMax)
                {
                    yMax = y;
                }
            }
        }

        private byte _correction;
    }
}
