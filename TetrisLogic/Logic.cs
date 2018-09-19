using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract;
using TetrisAbstract.Classes;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.Interfaces;

namespace TetrisLogic
{
    public class Logic : ITetrisLogic
    {
        public Logic()
        {
            _rotator = new FigureRotator();
            _mover = new FigureMover();
            _burner = new Burner();
            Subscribe();
        }

      

        public event Action ExchangeFigureEvent;
        public event Action LevelUpEvent;
        public event Action GOverEvent;
        public event EventHandler<SoundEventArgs> SndEvent;

        private void Subscribe()
        {
            if (LevelUpEvent != null)
            {
                _mover.LvlUpEvent += LevelUpEvent;
            }

            if (ExchangeFigureEvent != null)
            {
                _mover.ExchFigEvent += ExchangeFigureEvent;
            }

            if (GOverEvent != null)
            {
                _mover.GmOverEvent += GOverEvent;
            }
            _rotator.SoundEvent += SndEvent;
            _mover.SoundEvent += SndEvent;
            _mover.BurnLine += _burner.Burn;
            _burner.SoundEvent += SndEvent;
        }


        public void DeleteEmptyPoint(Figure currentFigure)
        {
            int num = FigureData.HeightWidth;
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                if (currentFigure.Body[i, 1] < num)
                {
                    num = currentFigure.Body[i, 1];
                }
            }
            for (int i = 0; i < FigureData.FigurePoints; i++)
            {
                currentFigure.Body[i, 1] -= (byte)num;
                currentFigure.Body[i, 0] += (FigureData.HeightWidth/2) + 1;
            }
        }

        public void Turn(GameBoardData gameBoard, Figure figure)
        {
            _rotator.Turn(gameBoard, figure);
            _rotator.SoundEvent += SndEvent;
        }

        public void Move(GameBoardData gameBoard, Figure currentFigure, Direction dir)
        {
            _mover.Move(gameBoard, ref currentFigure, dir);
        }

       
        private readonly FigureRotator _rotator;
        private readonly FigureMover _mover;
        private readonly Burner _burner;
    }
}
