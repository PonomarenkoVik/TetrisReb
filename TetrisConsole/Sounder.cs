using System.Media;
using TetrisAbstract.Enum;

namespace TetrisConsole
{
    class Sounder
{
        public void Play(object snd)
        {
            switch ((TypeAction)snd)
            {
                case TypeAction.BurnLine:
                    _player.SoundLocation = "burn.wav";
                    break;
                case TypeAction.Step:
                    _player.SoundLocation = "move.wav";
                    break;
                case TypeAction.Turning:
                    _player.SoundLocation = "turn.wav";
                    break;
            }
            _player.Play();
        }
        private readonly SoundPlayer _player = new SoundPlayer();
    }
}
