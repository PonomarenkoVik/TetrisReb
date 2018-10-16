using System;
using System.Drawing;
using System.Runtime.InteropServices;
using TetrisAbstract.Enum;
using TetrisAbstract.EventArgs;
using TetrisAbstract.GameClasses;

namespace TetrisConsole
{
    internal static class Drawer
    {
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteDC(IntPtr hDc);

        static IntPtr _hWnd = IntPtr.Zero;
        static IntPtr _hDc = IntPtr.Zero;

        public static void Draw(object sender, UpdateEventArgs args)
        {
            _hWnd = GetConsoleWindow();

            if (_hWnd != IntPtr.Zero)
            {
                _hDc = GetDC(_hWnd);
                if (_hDc != IntPtr.Zero)
                {                   
                    int size = 25;
                    Graphics consoleGraphics = Graphics.FromHdc(_hDc);
                    DrawGameData(args.GameBoardData, consoleGraphics);
                    CopyCurrentFigureToGameBoard(args);
                    DrawGameBoard(args.GameBoardData.Field, consoleGraphics, size);
                    DrawNextFigure(args.NextFigureData, consoleGraphics, size);
                    //font.Dispose();
                    //whitePen.Dispose();
                    //redPen.Dispose();
                }
                //ReleaseDC(_hWnd, _hDc);
                //DeleteDC(_hDc);
            }
        }

        private static void CopyCurrentFigureToGameBoard(UpdateEventArgs args)
        {
            TColor col = args.CurrentFigureData.Color;
            for (int i = 0; i < args.CurrentFigureData.Body.GetLength(0); i++)
            {
                byte x = args.CurrentFigureData.Body[i, 0];
                byte y = args.CurrentFigureData.Body[i, 1];
                args.GameBoardData.Field[x, y] = col;
            }
        }

        private static void DrawGameData(GameBoardData board, Graphics consoleGraphics)
        {
            int horizIndention = 315;
            int vertIndention = 200;
            try
            {
                var font = new Font("verdana", 13);
                consoleGraphics.FillRectangle(Brushes.RoyalBlue, new Rectangle(300, 190, 160,
                    160));
                consoleGraphics.DrawString(string.Format("Level: {0}", board.Level + 1), font, Brushes.White, horizIndention, 200);
                consoleGraphics.DrawString(string.Format("Line:   {0}", board.BurnedLine), font, Brushes.White, horizIndention, 40 + vertIndention);
                consoleGraphics.DrawString(string.Format("Score: {0}", board.Score), font, Brushes.White,
                    horizIndention, 80 + vertIndention);
            }
            catch (ExternalException)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        private static void DrawGameBoard(TColor[,] field, Graphics consoleGraphics, int size)
        {
            int horizIndention = 30;
            int vertIndention = 50;
            for (int i = 0; i < field.GetLength(1); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    try
                    {
                        if (field[j, i] != TColor.Empty)
                        {
                            consoleGraphics.FillEllipse(GetColor(field[j, i]), new Rectangle(
                                j * size + horizIndention, i * size + vertIndention, size,
                                size));
                        }
                        else
                        {
                            consoleGraphics.FillEllipse(Brushes.CornflowerBlue,
                                new Rectangle(j * size + horizIndention, i * size + vertIndention, size,
                                    size));
                        }
                    }
                    catch (ExternalException)
                    {
                        //Console.WriteLine(e);
                    }
                }
            }
        }

        private static void DrawNextFigure(FigureData fig, Graphics consoleGraphics, int size)
        {           
            int horizIndention = 325;
            int vertIndention = 50;

            TColor[,] rebuiltFigure = Rebuild(fig.Body, fig.Color);
            Brush col = GetColor(fig.Color);
            for (int i = 0; i < rebuiltFigure.GetLength(1); i++)
            {
                for (int j = 0; j < rebuiltFigure.GetLength(0); j++)
                {
                    try
                    {
                        if (rebuiltFigure[j, i] != TColor.Empty)
                        {
                            consoleGraphics.FillEllipse(col,
                                new Rectangle(j * size + horizIndention, i * size + vertIndention, size,
                                    size));
                        }
                        else
                        {
                            consoleGraphics.FillEllipse(Brushes.CornflowerBlue,
                                new Rectangle(j * size + horizIndention, i * size + vertIndention, size,
                                    size));
                        }
                    }
                    catch (ExternalException)
                    {
                        //Console.WriteLine(e);
                    }
                }
            }
        }

        public static void Draw(string str)
        {
            _hWnd = GetConsoleWindow();
            if (_hWnd != IntPtr.Zero)
            {
                _hDc = GetDC(_hWnd);
                if (_hDc != IntPtr.Zero)
                {
                    using (Graphics consoleGraphics = Graphics.FromHdc(_hDc))
                    {
                        Font font = new Font("verdana", 16);
                        consoleGraphics.DrawString(str, font, Brushes.White, 50, 300);
                        font.Dispose();
                    }
                }

                ReleaseDC(_hWnd, _hDc);
                DeleteDC(_hDc);
            }
        }

        public static void DrawInitialData()
        {
            _hWnd = GetConsoleWindow();

            if (_hWnd != IntPtr.Zero)
            {
                _hDc = GetDC(_hWnd);
                if (_hDc != IntPtr.Zero)
                {
                    Graphics consoleGraphics = Graphics.FromHdc(_hDc);
                    Font font;
                    try
                    {
                        font = new Font("verdana", 13);
                        consoleGraphics.FillRectangle(Brushes.RoyalBlue, new Rectangle(0, 0, 500,
                            800));
                        consoleGraphics.DrawString("S - Start, Q - Stop, P -Pause", font, Brushes.White, 100, 560);
                        consoleGraphics.DrawString("v - Down, < - Left, > - Right, Space - Turn", font, Brushes.White,
                            35, 580);
                        consoleGraphics.DrawString("Tetris 2.2", font, Brushes.White, 10, 10);
                        for (int i = 0; i < 5; i++)
                        {
                            consoleGraphics.DrawLine(new Pen(Color.White), 30 - i, 50, 30 - i, 550);
                            consoleGraphics.DrawLine(new Pen(Color.White), 280 + i, 50, 280 + i, 550);
                            consoleGraphics.DrawLine(new Pen(Color.White), 26, 50 - i, 284, 50 - i);
                            consoleGraphics.DrawLine(new Pen(Color.White), 26, 550 + i, 284, 550 + i);
                            consoleGraphics.FillRectangle(Brushes.CornflowerBlue, new Rectangle(30, 50, 250, 500));
                            consoleGraphics.FillRectangle(Brushes.CornflowerBlue, new Rectangle(325, 50, 100, 100));
                            consoleGraphics.DrawLine(new Pen(Color.White), 325 - i, 50, 325 - i, 150);
                            consoleGraphics.DrawLine(new Pen(Color.White), 425 + i, 50, 425 + i, 150);
                            consoleGraphics.DrawLine(new Pen(Color.White), 321, 50 - i, 429, 50 - i);
                            consoleGraphics.DrawLine(new Pen(Color.White), 321, 150 + i, 429, 150 + i);
                        }
                    }
                    catch (ExternalException)
                    {
                        //Console.WriteLine(e);
                        //throw;
                    }
                }
            }
        }


        private static TColor[,] Rebuild(byte[,] source, TColor col)
        {
            TColor[,] figResult = new TColor[FigureData.HeightWidth, FigureData.HeightWidth];
            for (int i = 0; i < source.GetLength(0); i++)
            {
                figResult[source[i, 0], source[i, 1]] = col;
            }
            return figResult;
        }

        public static Brush GetColor(TColor source)
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
    }
}
