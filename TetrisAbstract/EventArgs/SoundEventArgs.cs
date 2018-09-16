using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAbstract.Enum;

namespace TetrisAbstract.EventArgs
{
    public class SoundEventArgs : System.EventArgs
    {
        public SoundEventArgs(GameSound soung)
        {
            Sound = soung;
        }
        public GameSound Sound { get; set; }
    }
}
