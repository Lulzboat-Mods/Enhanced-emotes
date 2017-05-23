using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emotes
{
    public class Emotes : BaseScript
    {
        static string emotesText = string.Empty;

        static List<Emote> emotes = new List<Emote>()
        {
            new Emote(EmoteTypeEnum.WORLD_HUMAN_COP_IDLES, "/cop", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_PICNIC, "/sit", 0, true),
            new Emote(EmoteTypeEnum.CODE_HUMAN_MEDIC_KNEEL, "/kneel", 0, true),
            new Emote(EmoteTypeEnum.CODE_HUMAN_MEDIC_TEND_TO_DEAD, "/medic", 0, true),
            new Emote(EmoteTypeEnum.CODE_HUMAN_MEDIC_TIME_OF_DEATH, "/notepad", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_CAR_PARK_ATTENDANT, "/traffic", 0, false),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_PAPARAZZI, "/photo", 0, false),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_CLIPBOARD, "/clipboard", 0, false),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_LEANING, "/lean", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_SMOKING, "/smoke", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_DRINKING, "/drink", 0, true)
        };

        public Emotes()
        {
            foreach (Emote emote in emotes)
            {
                if (emotesText == string.Empty)
                    emotesText += emote.Command;
                else
                    emotesText += ", " + emote.Command;
            }

            EventHandlers["chatMessage"] += new Action<dynamic, dynamic, dynamic>(ChatMessage);

            Tick += OnTick;
        }

        public async Task OnTick()
        {
            // Show notification when Z was pressed
            if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
                CancelEmote();
        }

        #region Methods

        void PrintEmoteList()
        {
            TriggerEvent("chatMessage", "EMOTES", new int[] { 255, 0, 0 } , emotesText);
        }

        void CancelEmote()
        {
            Function.Call(Hash.CLEAR_PED_TASKS, Game.PlayerPed);
        }

        #endregion

        #region Events

        void ChatMessage(dynamic _source, dynamic _name, dynamic _message)
        {
            string message = (string)_message;

            Screen.ShowNotification(message);

            if (message.StartsWith("/emote"))
                PrintEmoteList();
            else if (message.StartsWith("/cancelemote"))
                CancelEmote();
            else
            {
                Emote emote = emotes.FirstOrDefault(o => o.Command == message);
                if (emote != null && Game.PlayerPed != null)
                {
                    Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, Game.PlayerPed, emote.EmoteType.ToString(), 0, emote.ToRename);
                    Screen.ShowNotification(emote.Description);
                }
            }
        }

        #endregion
    }
}
