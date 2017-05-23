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
        public LanguageEnum Language { get; set; }
        public string Command { get; set; }
        public string Description { get; set; }
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }

        public Emote(EmoteTypeEnum _emoteType, LanguageEnum _language, string _command, int _intValue, bool _boolValue)
        {
            EmoteType = _emoteType;
            Language = _language;
            Command = _command;
            Description = Descriptions.DescriptionStrings[(int)EmoteType][(int)Language];
            IntValue = _intValue;
            BoolValue = _boolValue;
        }
    }
}
