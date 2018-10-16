using System;
using System.Threading.Tasks;
using System.Timers;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.Interfaces;
using TetrisDataLayer;
using TetrisGame;
using TetrisLogic;

namespace TetrisConsole
{
    class TetrisConsole
    {
        static TetrisConsole()
        {
            _velocity = 1;
            TetrisGame = new Game(new TetrisRepository(), new Logic());
            Sounder = new Sounder();
            Timer = new Timer(600 / _velocity);
        }

        static void Main(string[] args)
        {
            Console.WindowWidth = 56;
            Console.WindowHeight = 40;
            Console.CursorVisible = false;
            TetrisGame.ActionEvent += OnAction;
            TetrisGame.GameOverEvent += GameOver;
            TetrisGame.UpdateDataEvent += OnUpdate;
            Timer.Elapsed += TimerOnTick;
            Drawer.DrawInitialData();
            StartKeyControl();
        }

        private static void StartKeyControl()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    Direction dir = Direction.Empty;
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            dir = Direction.Down;
                            break;
                        case ConsoleKey.LeftArrow:
                            dir = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            dir = Direction.Right;
                            break;
                        case ConsoleKey.Spacebar:
                            if (Timer.Enabled)
                            {
                                TetrisGame.Turn(); 
                            }
                            break;
                        case ConsoleKey.P:
                            PauseGame_Click();
                            break;
                        case ConsoleKey.S:
                            StartGame_OnClick();
                            break;
                        case ConsoleKey.Q:
                            StopGame_Click();
                            break;
                        default:
                            dir = Direction.Empty;
                            break;
                    }
                    if (dir != Direction.Empty && Timer.Enabled)
                    {
                        TetrisGame.Move(dir);
                    }
                }
            }
        }


        private static void StartGame_OnClick()
        {
            if (!_gameIsEnable)
            {
                _gameIsEnable = true;
                Drawer.DrawInitialData();
                TetrisGame.Start();
                Timer.Start();
            }
            
            

        }
        private static void StopGame_Click()
        {
            GameOver();
        }

        private static void PauseGame_Click()
        {
            if (Timer.Enabled)
            {
                Drawer.Draw("         Пауза");
                Timer.Stop();
            }
            else
            {
                Drawer.DrawInitialData();
                TimerOnTick(null, null);
                Timer.Start();
            }
        }


        private static void TimerOnTick(object sender, EventArgs eventArgs)
        {
            TetrisGame.Move(Direction.Down);
        }

        private static void OnAction(ActionEventArgs e)
        {
            Sound(e.Action);
        }

        private static void Sound(TypeAction a)
        {
            Task.Factory.StartNew(() => Sounder.Play(a), TaskCreationOptions.AttachedToParent);
        }

        private static void GameOver()
        {
            if (_gameIsEnable)
            {
                Timer.Stop();
                _gameIsEnable = false;
                Drawer.DrawInitialData();
                Drawer.Draw("     Game over"); 
            }
        }

        private static void OnUpdate(object sender, UpdateEventArgs e)
        {
            if (Math.Abs(_velocity - e.Velocity) > 0.001)
            {
                Timer.Interval = (int)(600 / _velocity);
                _velocity = e.Velocity;
            }
            Drawer.Draw(sender, e);
        }

        private static readonly ITetrisGame TetrisGame;
        private static readonly Sounder Sounder;
        private static float _velocity;
        private static readonly Timer Timer;
        private static bool _gameIsEnable;
    }
}
