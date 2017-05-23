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
        public EmoteTypeEnum EmoteType { get; set; }
        public string Command { get; set; }
        public string Description { get; set; }
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }

        public Emote(EmoteTypeEnum _emoteType, string _command, int _intValue, bool _boolValue)
        {
            EmoteType = _emoteType;
            Command = _command;
            Description = Descriptions.DescriptionStrings[(int)EmoteType][(int)LanguageEnum.EN];
            IntValue = _intValue;
            BoolValue = _boolValue;
        }
    }
}
