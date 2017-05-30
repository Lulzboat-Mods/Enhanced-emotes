using Emotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotes
{
    public class Emote
    {
        public string EmoteType { get; set; }
        public string Command { get; set; }
        public string Description { get; set; }
        public int Delay { get; set; }
        public bool PlayEnterAnim { get; set; }
    }
}
