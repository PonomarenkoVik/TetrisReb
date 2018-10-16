using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;
using TetrisAbstract.Interfaces;
using TetrisDataLayer;
using TetrisGame;
using TetrisLogic;
using Color = System.Drawing.Color;

namespace TetrisForms
{
    public partial class TetrisForm : Form
    {
        public TetrisForm()
        {
            InitializeComponent();
            _velocity = 1;
            _tetrisGame = new Game(new TetrisRepository(), new Logic());
            _sounder = new Sounder();
            _tetrisGame.ActionEvent += OnAction;
            _tetrisGame.GameOverEvent += OnGameOver;
            _tetrisGame.UpdateDataEvent += OnUpdate;
            _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / _velocity))};
            _timer.Tick += TimerOnTick;
            _graphicGameBoard = GBoard.CreateGraphics();
            _graphicNextFigure = NFigureBoard.CreateGraphics();
            SetMenuStatus(false);
        }


        private readonly Color _backColor = Color.FromArgb(182, 192, 235);
        const byte SizePoint = 25;




        private void TimerOnTick(object sender, EventArgs e)
        {
            _tetrisGame.Move(Direction.Down);
        }

        private void OnUpdate(object sender, UpdateEventArgs e)
        {
            if (Math.Abs(_velocity - e.Velocity) > 0.001)
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / _velocity));
                _velocity = e.Velocity;
            }

            Score.Text = e.GameBoardData.Score.ToString();
            Level.Text = (e.GameBoardData.Level + 1).ToString();
            BurnedLine.Text = e.GameBoardData.BurnedLine.ToString();
            ShowNextFigure(Rebuild(e.NextFigureData.Body, e.NextFigureData.Color), GetColor(e.NextFigureData.Color));
            CopyCurrentFigureToGameBoard(e);
            ShowGameBoard(e);
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

       


        private void StripMenuStartItem_Click(object sender, EventArgs e)
        {
            MessageLabel.Visible = false;    
            SetMenuStatus(true);
            _tetrisGame.Start();
            _timer.Start();
        }

        private void StripMenuStopItem_Click(object sender, EventArgs e)
        {
            GameOver();
        }

        private void StripMenuPauseItem_Click(object sender, EventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                SetMenuStatus(false);
                StripMenuStartItem.Enabled = false;
                StripMenuPauseItem.Enabled = true;
                MessageLabel.Text = "     Пауза";
                MessageLabel.Visible = true;

            }
            else
            {
                SetMenuStatus(true);
                MessageLabel.Visible = false;
                _timer.Start();
            }
        }

        private void MenuSaveOptionsItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StripMenuExitItem_Click(object sender, EventArgs e)
        {
            _graphicGameBoard.Dispose();
            _graphicNextFigure.Dispose();
            Close();
        }

        private void StripMenuInformationItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Left - <\nRight - >\nDown - v\nTurn - Space\nPause - P", "Control");
        }

        private void StripMenuSaveGameItem_Click(object sender, EventArgs e)
        {
            _tetrisGame.Save();
        }

        private void StripMenuOpenGameItem_Click(object sender, EventArgs e)
        {
            var savePoints = _tetrisGame.GetSavePoints();
            OpenGameForm form = new OpenGameForm(savePoints)
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            };
            form.ChooseSavePointEvent += args => _tetrisGame.OpenGame(args);
            form.ShowDialog();
        }

       

        private void TetrisForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (_timer.IsEnabled)
                    {
                        TMove(Direction.Down);
                    }
                    break;
                case Keys.Left:
                    if (_timer.IsEnabled)
                    {
                        TMove(Direction.Left);
                    }

                    break;
                case Keys.Right:
                    if (_timer.IsEnabled)
                    {
                        TMove(Direction.Right);
                    }
                    break;
                case Keys.Space:
                    if (_timer.IsEnabled)
                    {
                        _tetrisGame.Turn();
                    }
                    break;
                case Keys.P:
                    StripMenuPauseItem_Click(null, null);
                    break;
            }
        }

        private void TMove(Direction dir)
        {
            _tetrisGame.Move(dir);
        }

        private void GameOver()
        {
            _timer.Stop();
            MessageLabel.Text = "Конец игры";
            MessageLabel.Visible = true;
            SetMenuStatus(false);
        }
        private void SetMenuStatus(bool status)
        {
            StripMenuStartItem.Enabled = !status;
            StripMenuExitItem.Enabled = !status;
            StripMenuInformationItem.Enabled = !status;
            StripMenuPauseItem.Enabled = status;
            StripMenuStopItem.Enabled = status;
            StripMenuSaveOptions.Enabled = !status;

        }

        #region Painting

        private void ShowNextFigure(TColor[,] body, Brush col)
        {
            for (byte i = 0; i < body.GetLength(1); i++)
            {
                for (byte j = 0; j < body.GetLength(0); j++)
                {
                    if (body[j, i] != TColor.Empty )
                    {
                        _graphicNextFigure.FillEllipse(col,
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                    else
                    {
                        _graphicNextFigure.FillEllipse(new SolidBrush(_backColor),
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                }
            }
        }

        private void ShowGameBoard(UpdateEventArgs arg)
        {
            int boardWidth = arg.GameBoardData.Field.GetLength(0);
            int boardHight = arg.GameBoardData.Field.GetLength(1);
            for (byte i = 0; i < boardHight; i++)
            {
                for (byte j = 0; j < boardWidth; j++)
                {
                    if (arg.GameBoardData.Field[j, i] != TColor.Empty)
                    {
                        _graphicGameBoard.FillEllipse(GetColor(arg.GameBoardData.Field[j, i]),
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                    else
                    {
                        _graphicGameBoard.FillEllipse(new SolidBrush(_backColor),
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                }
            }
        }

        #endregion


        private void Sound(TypeAction a)
        {
            Task.Factory.StartNew(() => _sounder.Play(a), TaskCreationOptions.AttachedToParent);
        }
        private TColor[,] Rebuild(byte[,] source, TColor col)
        {
            TColor[,] figResult = new TColor[FigureData.HeightWidth, FigureData.HeightWidth];
            for (int i = 0; i < source.GetLength(0); i++)
            {
                figResult[source[i, 0], source[i, 1]] = col;
            }
            return figResult;
        }

        private void CopyCurrentFigureToGameBoard(UpdateEventArgs args)
        {
            TColor col = args.CurrentFigureData.Color;
            for (int i = 0; i < args.CurrentFigureData.Body.GetLength(0); i++)
            {
                byte x = args.CurrentFigureData.Body[i, 0];
                byte y = args.CurrentFigureData.Body[i, 1];
                args.GameBoardData.Field[x, y] = col;
            }
        }

        private  Brush GetColor(TColor source)
        {
            Brush color;
            switch (source)
            {
                case TColor.Brown:
                    color = Brushes.Brown;
                    break;
                case TColor.Red:
                    color = Brushes.Red;
                    break;
                case TColor.BlueViolet:
                    color = Brushes.BlueViolet;
                    break;
                case TColor.Green:
                    color = Brushes.Green;
                    break;
                case TColor.Orange:
                    color = Brushes.DarkOrange;
                    break;
                case TColor.Pink:
                    color = Brushes.DeepPink;
                    break;
                default:
                    color = Brushes.Black;
                    break;
            }
            return color;
        }


        private readonly Graphics _graphicGameBoard;
        private readonly Graphics _graphicNextFigure;
        private readonly ITetrisGame _tetrisGame;
        private readonly DispatcherTimer _timer;
        private readonly Sounder _sounder;
        private float _velocity;
    }
}
