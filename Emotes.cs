using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyJson;

namespace Emotes
{
    public class Emotes : BaseScript
    {
        string emotesText = string.Empty;
        List<Emote> emotes = new List<Emote>();
        List<string> errors = new List<string>();
        Config config = new Config();

        public Emotes()
        {
            /* 
             * MENU LATER
             */
            /*
            menu = new UIMenu("Emotes", "");
            menu.OnItemSelect += OnItemSelect;
            menu.Visible = false;

            foreach (Emote emote in emotes)
            {
                menu.AddItem(new UIMenuItem(emote.Description));
            }
            */

            LoadConfig();
            LoadJson();

            // FiveM related things
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
        
        void LoadConfig()
        {
            string configFile = Function.Call<string>(Hash.LOAD_RESOURCE_FILE, "emotes", "config.ini");
            Debug.WriteLine("{0}", configFile);
            string[] lines = configFile.Split('\n');

            foreach (string line in lines)
            {
                string[] content = line.Split('=');

                if (content[0].ToLower() == "jsonfile")
                    config.JsonFile = content[1];
            }
        }

        void LoadJson()
        {
            Debug.WriteLine("{0}", config.JsonFile);
            
            string jsonString = Function.Call<string>(Hash.LOAD_RESOURCE_FILE, "emotes", "english.json"); // I have to find how to dynamicly load files, feels like i can only pass const strings
            Debug.WriteLine("{0}", jsonString);

            Dictionary<string, object> jsonObject = (Dictionary<string, object>)jsonString.FromJson<object>();

            List<object> emoteList = (List<object>)jsonObject["emotes"];

            foreach (Dictionary<string, object> emote in emoteList)
            {
                emotes.Add(new Emote()
                {
                    EmoteType = (string)emote["hash"],
                    Command = (string)emote["title"],
                    Description = (string)emote["description"],
                    IntValue = 0,
                    BoolValue = (bool)emote["boolean"]
                });
            }
            errors.Add((string)jsonObject["errorVehicle"]);
            errors.Add((string)jsonObject["errorUnexpected"]);
            errors.Add((string)jsonObject["errorPlayerID"]);
            
            foreach (Emote emote in emotes)
            {
                if (emotesText == string.Empty)
                    emotesText += emote.Command;
                else
                    emotesText += ", " + emote.Command;
            }
        }

        void PrintEmoteList()
        {
            TriggerEvent("chatMessage", "ALERT", new int[] { 255, 0, 0 } , emotesText);
        }

        void CancelEmote()
        {
            Function.Call(Hash.CLEAR_PED_TASKS, Game.PlayerPed);
        }

        void PlayEmote(Emote emote)
        {
            if (Game.PlayerPed != null)
            {
                bool isInVehicle = Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Game.PlayerPed, true);

                if (!isInVehicle)
                {
                    Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, Game.PlayerPed, emote.EmoteType, emote.IntValue, emote.BoolValue);
                    Screen.ShowNotification(emote.Description);
                }
                else
                    Screen.ShowNotification(errors.ElementAt(0));
            }
            else
                Screen.ShowNotification(errors.ElementAt(2));
        }

        #endregion

        #region Events

        /*
        void OnItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            Emote emote = emotes.FirstOrDefault(o => o.Description == item.Text);

            if (emote != null)
                PlayEmote(emote);
            else
                Screen.ShowNotification(errors.ElementAt(1));

            menu.Visible = false;
        }
        */

        void ChatMessage(dynamic _source, dynamic _name, dynamic _message)
        {
            string message = (string)_message;
            string test = string.Empty;
            
            if (message.StartsWith("/emote"))
                PrintEmoteList();
            else if (message.StartsWith("/cancel"))
                CancelEmote();
            else
            {
                Emote emote = emotes.FirstOrDefault(o => o.Command == message);
                if (emote != null)
                    PlayEmote(emote);
            }
        }

        #endregion
    }
}
