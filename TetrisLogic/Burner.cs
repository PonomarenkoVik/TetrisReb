using System;
using TetrisAbstract.Enum;
using TetrisAbstract.GameClasses;

namespace TetrisLogic
{
    class Burner
    {
        public Burner()
        {
            _height = TetrisInitialData.FieldHeight;
            _width = TetrisInitialData.FieldWidth;
        }
        public event Action BurnLineEvent;
        public void Burn(GameBoardData board)
        {
            for (int i = 0; i < _height; i++)
            {
                bool lineBurn = true;
                for (int j = 0; j < _width; j++)
                {
                    if (board.Field[j, i] == TColor.Empty)
                    {
                        lineBurn = false;
                        break;
                    }
                }
                if (!lineBurn) continue;
                BurnLine(i, board);
            }
        }

        private void BurnLine(int line, GameBoardData board)
        {
            for (int i = line; i > 0; i--)
            {
                for (int j = 0; j < _width; j++)
                {
                    board.Field[j, i] = board.Field[j, i - 1];
                }
            }
            board.Score += 100;
            board.BurnedLine += 1;
            if (BurnLineEvent != null)
            {
                BurnLineEvent();
            }
        }

        private readonly int _height;
        private readonly int _width;
    }
}