using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;
using TetrisAbstract.Interfaces;
using TetrisDataLayer;
using TetrisGame;
using TetrisLogic;
using TetrisWPF.Extensions;
using TetrisWPF.Windows;

namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _velocity = 1;
            _tetrisGame = new Game(new TetrisRepository(), new Logic());
            _sounder = new Sounder();
            _tetrisGame.ActionEvent += OnAction;
            _tetrisGame.GameOverEvent += OnGameOver;
            _tetrisGame.UpdateDataEvent += OnUpdate;
            _timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, (int)(600/_velocity))};
            _timer.Tick += TimerOnTick;
            

        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            _tetrisGame.Move(Direction.Down);
        }

        const byte SizeRectangle = 25;
        const float Opac = 1f;
        const byte StrokeThickness = 1;
        private readonly SolidColorBrush _borderColor = new SolidColorBrush(Colors.Black);  
       

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Left - <\nRight - >\nDown - v\nTurn - Space\nPause - P", "Control");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (_timer.IsEnabled)
                    {
                        Move(Direction.Down);  
                    }
                    break;
                case Key.Left:
                    if (_timer.IsEnabled)
                    {
                        Move(Direction.Left);
                    }
                    break;
                case Key.Right:
                    if (_timer.IsEnabled)
                    {
                        Move(Direction.Right);
                    }
                    break;
                case Key.Space:
                    if (_timer.IsEnabled)
                    {
                        _tetrisGame.Turn();
                    }
                    break;
                case Key.P:
                    PauseGame_Click(null, null);
                    break;
            }
        }

        private void Move(Direction dir)
        {
            _tetrisGame.Move(dir); 
        }

        private void StartGame_OnClick(object sender, RoutedEventArgs e)
        {
            SetMenuItemStatus(true);
            RectPause.Visibility = Visibility.Hidden;
            _tetrisGame.Start();
            _timer.Start();
           
           
        }

        private void StopGame_Click(object sender, RoutedEventArgs e)
        {
            GameOver();
        }

        private void PauseGame_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                SetMenuItemStatus(false);
                StartGame.IsEnabled = false;
                PauseGame.IsEnabled = true;
                PauseAnimation.Content = "Pause";
                RectPause.Visibility = Visibility.Visible;
                if (!GameBoard.Children.Contains(RectPause))
                {
                    GameBoard.Children.Remove(RectPause);
                    GameBoard.Children.Add(RectPause);
                }
               
            }
            else
            {
                SetMenuItemStatus(true);
                RectPause.Visibility = Visibility.Hidden;
                _timer.Start();
            }
        }

        private void OnUpdate(object sender, UpdateEventArgs e)
        {
            if (Math.Abs(_velocity - e.Velocity) > 0.001)
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / _velocity));
                _velocity = e.Velocity;
            }

            Score.Content = e.GameBoardData.Score;
            Level.Content = e.GameBoardData.Level + 1;
            Line.Content = e.GameBoardData.BurnedLine;
            PaintGameBoard(e.GameBoardData.Field);
            NextFigureBoard.Children.Clear();
            PaintFigure(e.NextFigureData);
            PaintFigure(e.CurrentFigureData);
        }

        private void OnGameOver()
        {
            GameOver();
        }

       

        private void OnAction(ActionEventArgs e)
        {
            if (e.Direction != Direction.Down)
            {
                Sound(e.Action);
            }
        }

        private void Sound(TypeAction a)
        {
            Task.Factory.StartNew(() => _sounder.Play(a), TaskCreationOptions.AttachedToParent);
        }


        private void OpenGame_OnClick(object sender, RoutedEventArgs e)
        {
            var savePoints = _tetrisGame.GetSavePoints();
            OpenGameWindow window = new OpenGameWindow(savePoints)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ChooseSavePointEvent = args => _tetrisGame.OpenGame(args)
            };
            window.ShowDialog();
        }

        private void SaveGame_OnClick(object sender, RoutedEventArgs e)
        {
            //if (_connString == null)
            //{
            //    SetConectionOption();
            //}
            _tetrisGame.Save();

        }

        private void Options_OnClick(object sender, RoutedEventArgs e)
        {
            SetConectionOption();
        }


        private void SetConectionOption()
        {
            throw new NotImplementedException();
        }

        private void GameOver()
        {
            _timer.Stop();
            PauseAnimation.Content = "Game Over";
            RectPause.Visibility = Visibility.Visible;
            if (!GameBoard.Children.Contains(RectPause))
            {
                GameBoard.Children.Remove(RectPause);
                GameBoard.Children.Add(RectPause);
            }

            SetMenuItemStatus(false);
        }

        private void SetMenuItemStatus(bool status)
        {
            StopGame.IsEnabled = status;
            StartGame.IsEnabled = !status;
            PauseGame.IsEnabled = status;
            Information.IsEnabled = !status;
            OpenGame.IsEnabled = !status;
            SaveGame.IsEnabled = !status;
            Options.IsEnabled = !status;

            //Dispatcher.CurrentDispatcher.Invoke(() => { });

        }

        #region Showing


        private void PaintGameBoard(TColor[,] board)
        {

            int boardWidth = board.GetLength(0);
            int boardHight = board.GetLength(1);
            GameBoard.Children.Clear();
            for (byte i = 0; i < boardHight; i++)
            {
                for (byte j = 0; j < boardWidth; j++)
                {
                    if (board[j, i] != TColor.Empty)
                    {
                        SolidColorBrush color = board[j, i].ToSolidColorBrush();
                        GameBoard.Children.Add(CreateRectangle(color, j, i));
                    }
                }
            }
        }

        private void PaintFigure(FigureData figure)
        {
            SolidColorBrush color = figure.Color.ToSolidColorBrush();
            for (int i = 0; i < figure.Body.GetLength(0); i++)
            {
                byte x = figure.Body[i, 0];
                byte y = figure.Body[i, 1];
                if (figure.IsCurrent)
                {
                    GameBoard.Children.Add(CreateRectangle(color, x, y));
                }
                else
                {
                    NextFigureBoard.Children.Add(CreateRectangle(color, x, y));
                }              
            }
        }

        private Rectangle CreateRectangle(SolidColorBrush color, byte j, byte i)
        {
            Rectangle rectangle = new Rectangle
            {
                StrokeThickness = StrokeThickness,
                Stroke = _borderColor,
                Width = SizeRectangle,
                Height = SizeRectangle,
                Opacity = Opac,
                Fill = color
            };

            rectangle.SetValue(Canvas.LeftProperty, j * (double)SizeRectangle);
            rectangle.SetValue(Canvas.TopProperty, i * (double)SizeRectangle);
            return rectangle;
        }

        #endregion

        private readonly ITetrisGame _tetrisGame;
        private readonly DispatcherTimer _timer;
        private readonly Sounder _sounder;
        private float _velocity;
    }
}
