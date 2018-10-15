using System;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;
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
        public event Action<ActionEventArgs> ActionEvent;

        private void Subscribe()
        {
            _rotator.RotateEvent += OnRotateHandler;
            _mover.StepEvent += OnStepHandler;
            _mover.BurnLine += OnBurnHandler;
            _burner.BurnLineEvent += OnBurnedHandler;
            _mover.GmOverEvent += OnGOverHandler;
            _mover.ExchFigEvent += OnExchangeFigureHandler;
            _mover.LvlUpEvent += OnLevelUpHandler;
        }

        private void OnLevelUpHandler()
        {
            if (LevelUpEvent != null)
            {
                LevelUpEvent();
            }
        }

        private void OnExchangeFigureHandler()
        {
            if (ExchangeFigureEvent != null)
            {
                ExchangeFigureEvent();
            }
        }

        private void OnGOverHandler()
        {
            if (GOverEvent != null)
            {
                GOverEvent();
            }
        }

        private void OnBurnedHandler()
        {
            if (ActionEvent != null)
            {
                ActionEvent(new ActionEventArgs(TypeAction.BurnLine));
            }
        }

        private void OnBurnHandler(GameBoardData gameBoard)
        {
            _burner.Burn(gameBoard);
        }

        private void OnStepHandler(object sender, ActionEventArgs e)
        {
            if (ActionEvent != null)
            {
                ActionEvent(e);
            }
        }

        private void OnRotateHandler()
        {
            if (ActionEvent != null)
            {
                ActionEvent(new ActionEventArgs(TypeAction.Turning));
            }
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
                currentFigure.Body[i, 0] += (FigureData.HeightWidth / 2) + 1;
            }
        }

        public void Turn(GameBoardData gameBoard, Figure figure)
        {
            _rotator.Turn(gameBoard, figure);
        }

        public void Move(GameBoardData gameBoard, ref Figure currentFigure, Direction dir)
        {
            _mover.Move(gameBoard, ref currentFigure, dir);
        }


        private readonly FigureRotator _rotator;
        private readonly FigureMover _mover;
        private readonly Burner _burner;
    }
}