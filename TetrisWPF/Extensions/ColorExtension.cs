using System.Windows.Media;
using TetrisAbstract.Enum;

namespace TetrisWPF.Extensions
{
    public static class ColorExtension
    {
        public static SolidColorBrush ToSolidColorBrush(this TColor source)
        {
            SolidColorBrush color = null;
            switch (source)
            {
                case TColor.Empty:
                    color = new SolidColorBrush(Colors.Black);
                    break;
                case TColor.Brown:
                    color = new SolidColorBrush(Colors.SaddleBrown);
                    break;
                case TColor.Red:
                    color = new SolidColorBrush(Colors.Red);
                    break;
                case TColor.BlueViolet:
                    color = new SolidColorBrush(Colors.BlueViolet);
                    break;
                case TColor.Green:
                    color = new SolidColorBrush(Colors.LimeGreen);
                    break;
                case TColor.Orange:
                    color = new SolidColorBrush(Colors.Orange);
                    break;
                case TColor.Pink:
                    color = new SolidColorBrush(Colors.DeepPink);
                    break;
            }
            return color;
        }
    }
}
