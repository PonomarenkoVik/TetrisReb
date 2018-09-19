using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;

namespace TetrisLogic
{
    class Burner
    {
        public Burner()
        {
            _height = TetrisInitialData.FieldHeight;
            _width = TetrisInitialData.FieldWidth;
        }
        public event EventHandler<SoundEventArgs> SoundEvent;
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
            if (SoundEvent != null)
            {
                SoundEvent(this, new SoundEventArgs(GameSound.BurnLine));
            }
        }

        private readonly int _height;
        private readonly int _width;
    }
}
