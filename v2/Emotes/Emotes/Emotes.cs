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
            new Emote(EmoteTypeEnum.WORLD_HUMAN_COP_IDLES, LanguageEnum.EN, "/cop", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_PICNIC, LanguageEnum.EN, "/sit", 0, true),
            new Emote(EmoteTypeEnum.CODE_HUMAN_MEDIC_KNEEL, LanguageEnum.EN, "/kneel", 0, true),
            new Emote(EmoteTypeEnum.CODE_HUMAN_MEDIC_TEND_TO_DEAD, LanguageEnum.EN, "/medic", 0, true),
            new Emote(EmoteTypeEnum.CODE_HUMAN_MEDIC_TIME_OF_DEATH, LanguageEnum.EN, "/notepad", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_CAR_PARK_ATTENDANT, LanguageEnum.EN, "/traffic", 0, false),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_PAPARAZZI, LanguageEnum.EN, "/photo", 0, false),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_CLIPBOARD, LanguageEnum.EN, "/clipboard", 0, false),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_LEANING, LanguageEnum.EN, "/lean", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_SMOKING, LanguageEnum.EN, "/smoke", 0, true),
            new Emote(EmoteTypeEnum.WORLD_HUMAN_DRINKING, LanguageEnum.EN, "/drink", 0, true)
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
            // Cancel emote when player is moving
            if (Game.IsControlJustReleased(0, Control.MoveUpOnly) ||
                Game.IsControlJustReleased(0, Control.MoveDownOnly) ||
                Game.IsControlJustReleased(0, Control.MoveLeftOnly) ||
                Game.IsControlJustReleased(0, Control.MoveRightOnly))
            {
                CancelEmote();
            }
        }

        #region Methods

        void PrintEmoteList()
        {
            TriggerEvent("chatMessage", "ALERT", new int[] { 255, 0, 0 } , emotesText);
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
            
            if (message.StartsWith("/emote"))
                PrintEmoteList();
            else if (message.StartsWith("/cancel"))
                CancelEmote();
            else
            {
                Emote emote = emotes.FirstOrDefault(o => o.Command == message);
                if (emote != null && Game.PlayerPed != null)
                {
                    bool isInVehicle = Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Game.PlayerPed, true);
                    
                    if (!isInVehicle)
                    {
                        Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, Game.PlayerPed, emote.EmoteType.ToString(), emote.IntValue, emote.BoolValue);
                        Screen.ShowNotification(emote.Description);
                    }
                    else
                        Screen.ShowNotification(Descriptions.ErrorStrings[(int)LanguageEnum.EN]);
                }
            }
        }

        #endregion
    }
}
