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
using TetrisGame.Classes;
using TetrisGame.Extensions;

namespace TetrisGame
{
    public class Game : ITetrisGame, IDisposable
    {
        public Game(IDataTetrisRepository repository, ITetrisLogic logic)
        {
            _gameBoard = new GameBoardData();
            _repository = repository;
            _logic = logic;
            _figCreator = new FigureCreator();
            _logic.ExchangeFigureEvent += ExchangeFigureEventFigures;
            _logic.LevelUpEvent += LevelUpEvent;
            _logic.GameOverEvent += GameOver;
        }


        public event Action GameOverEvent;
        public event EventHandler<UpdateEventArgs> UpdateDataEvent;
        public event EventHandler<SoundEventArgs> SoundEvent;


        public void Start()
        {
            if (_currentFigure == null)
            {
                InitializeGameBoard();
                InitializeFigures();
            }

            Update();
        }

        public void Move(Direction dir)
        {
            _logic.Move(_gameBoard, _currentFigure, dir);
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
        }

        #endregion


        public void Dispose()
        {
            _repository.Dispose();
        }





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
                for (int j = 0; j < TetrisInitialData.FieldHeight; j++)
                {
                    _gameBoard.Field[j, i] = TColor.Empty;
                }
            }
        }

        private void Update()
        {
            if (UpdateDataEvent != null)
            {
               UpdateDataEvent(this, new UpdateEventArgs(_gameBoard, _currentFigure.ToFigureData(true), _nextFigure.ToFigureData(false), _velocity));
            }
        }

        private void ExchangeFigureEventFigures()
        {
            _currentFigure = (Figure)_nextFigure.Clone();
            _logic.DeleteEmptyPoint(_currentFigure);
            _nextFigure = _figCreator.GetNewFigure();
            Update();
        }

        private void LevelUpEvent()
        {
            _gameBoard.Level++;
            SetVelocity();
            FieldInitialize();
            _gameBoard.Field = LevelData.GetLevelFilling(_gameBoard.Level);
            Update();
        }

        private void SetVelocity()
        {
            _velocity = LevelData.GetVelocity(_gameBoard.Level);
        }

        private void GameOver()
        {
            if (GameOverEvent != null)
            {
                GameOverEvent();
            }      
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
