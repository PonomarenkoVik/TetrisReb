using System;
using System.Collections.Generic;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;
using TetrisAbstract.Interfaces;
using TetrisGame.Classes;
using TetrisGame.Data;
using TetrisGame.Extensions;

namespace TetrisGame
{
    public class Game : ITetrisGame
    {
        public Game(IDataTetrisRepository repository, ITetrisLogic logic)
        {           
            _repository = repository;
            _logic = logic;
            _gameBoard = new GameBoardData();
            _figCreator = new FigureCreator();
            SetVelocity();
            Subscribe();
        }

        public event Action GameOverEvent;
        public event EventHandler<UpdateEventArgs> UpdateDataEvent;
        public event Action<ActionEventArgs> ActionEvent;

        private void Subscribe()
        {
            _logic.ExchangeFigureEvent += ExchangeFigureEventFigures;
            _logic.LevelUpEvent += LevelUpEvent;
            _logic.GOverEvent += LogicOnGOverEvent;
            _logic.ActionEvent += LogicOnActionEvent;    
        }

        private void LogicOnActionEvent(ActionEventArgs actionEventArgs)
        {
            if (ActionEvent != null)
            {
               ActionEvent(actionEventArgs);
            }
        }

        private void LogicOnGOverEvent()
        {
            if (GameOverEvent != null)
            {
                GameOverEvent();
            }
        }


        public void Start()
        {
            InitializeGameBoard();
            InitializeFigures();
            Update();
        }

        public void Move(Direction dir)
        {
            _logic.Move(_gameBoard,ref _currentFigure, dir);
            Update();
        }


        public void Turn()
        {
            _logic.Turn(_gameBoard, _currentFigure);
            Update();
        }


        #region ReadSave section

        public void Save()
        {
            _gameBoard.Date =  DateTime.Now;
            _repository.Save(_gameBoard, _currentFigure.ToFigureData(true), _nextFigure.ToFigureData(false));
        }

        public List<GameBoardData> GetSavePoints()
        {
            return _repository.GetSavePoints();
        }

        public void OpenGame(int idSavePoint)
        {
            _gameBoard = _repository.GetGameBoard(idSavePoint);
            _currentFigure = _repository.GetFigure(idSavePoint, true).ToFigureData();
            _nextFigure = _repository.GetFigure(idSavePoint, false).ToFigureData();
            Update();
        }

        #endregion



        private void InitializeFigures()
        {
            _currentFigure = _figCreator.GetNewFigure();
            _logic.DeleteEmptyPoint(_currentFigure);
            _nextFigure = _figCreator.GetNewFigure();
        }

        private void InitializeGameBoard()
        {
            _gameBoard.BurnedLine = 0;
            _gameBoard.Level = 0;
            _gameBoard.Score = 0;
            FieldInitialize();
        }

        private void FieldInitialize()
        {
            _gameBoard.Field = new TColor[TetrisInitialData.FieldWidth, TetrisInitialData.FieldHeight];
            for (int i = 0; i < TetrisInitialData.FieldHeight; i++)
            {
                for (int j = 0; j < TetrisInitialData.FieldWidth; j++)
                {
                    _gameBoard.Field[j, i] = TColor.Empty;
                }
            }
        }

        private void Update()
        {
            if (UpdateDataEvent != null && _currentFigure != null)
            {
               UpdateDataEvent(this, new UpdateEventArgs(_gameBoard, _currentFigure.ToFigureData(true), _nextFigure.ToFigureData(false), _velocity));
            }
        }

        private void ExchangeFigureEventFigures()
        {
            _currentFigure = (Figure)_nextFigure.Clone();
            _logic.DeleteEmptyPoint(_currentFigure);
            _nextFigure = _figCreator.GetNewFigure();

        }

        private void LevelUpEvent()
        {
            _gameBoard.Level++;
            SetVelocity();
            FieldInitialize();
            LevelData.GetLevelFilling(_gameBoard);
            Update();
        }

        private void SetVelocity()
        {
            _velocity = LevelData.GetVelocity(_gameBoard.Level);
        }

       



        private GameBoardData _gameBoard;
        private Figure _currentFigure;
        private Figure _nextFigure;
        private float _velocity;
        private readonly IDataTetrisRepository _repository;
        private readonly ITetrisLogic _logic;
        private readonly FigureCreator _figCreator;
    }
}
