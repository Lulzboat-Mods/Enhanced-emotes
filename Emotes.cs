using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using NativeUI;
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

        MenuPool menuPool;
        UIMenu mainMenu;

        public Emotes()
        {
            // FiveM related things
            EventHandlers["onPlayerJoining"] += new Action<dynamic, dynamic>(OnPlayerJoining);
            Tick += OnTick;
        }

        public async Task OnTick()
        {
            //if (menuLoaded)
            menuPool.ProcessMenus();

            // Cancel emote when player is moving
            if (Game.IsControlJustReleased(0, Control.MoveUpOnly) ||
                Game.IsControlJustReleased(0, Control.MoveDownOnly) ||
                Game.IsControlJustReleased(0, Control.MoveLeftOnly) ||
                Game.IsControlJustReleased(0, Control.MoveRightOnly))
            {
                CancelEmote();
            }
            else if (Game.IsControlJustPressed(0, Control.ReplayShowhotkey))
                mainMenu.Visible = !mainMenu.Visible;
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
                {
                    config.JsonFile = content[1];
                    config.JsonFile = config.JsonFile.Replace("\n", "");
                }
            }
        }

        void LoadJson()
        {
            string jsonString = string.Empty;
            string fileName = string.Empty;

            foreach (char c in config.JsonFile)
            {
                if ((int)c != 13)
                    fileName += c;
            }
            
            jsonString = Function.Call<string>(Hash.LOAD_RESOURCE_FILE, "emotes", fileName);
            
            var jsonObject = (Dictionary<string, object>)jsonString.FromJson<object>();
            var emoteList = (List<object>)jsonObject["emotes"];

            foreach (Dictionary<string, object> emote in emoteList)
            {
                emotes.Add(new Emote()
                {
                    EmoteType = (string)emote["hash"],
                    Command = (string)emote["title"],
                    Description = (string)emote["description"],
                    Delay = (int)emote["delay"],
                    PlayEnterAnim = (bool)emote["playEnterAnim"]
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
                    Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, Game.PlayerPed, emote.EmoteType, emote.Delay, emote.PlayEnterAnim);
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
        
        void OnItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            Emote emote = emotes.FirstOrDefault(o => o.Description == item.Text);

            if (emote != null)
                PlayEmote(emote);
            else
                Screen.ShowNotification(errors.ElementAt(1));

            mainMenu.Visible = false;
        }

        // TODO: Should check if player already loaded
        void OnPlayerJoining(dynamic arg1, dynamic arg2)
        {
            // Récupère les configs et la liste d'emote
            LoadConfig();
            LoadJson();

            // Crée le menu
            menuPool = new MenuPool();
            mainMenu = new UIMenu("Emotes", "Select an emote");
            mainMenu.MouseControlsEnabled = false;
            menuPool.Add(mainMenu);
            mainMenu.OnItemSelect += OnItemSelect;

            foreach (Emote emote in emotes)
            {
                mainMenu.AddItem(new UIMenuItem(emote.Description));
            }
            menuPool.RefreshIndex();
        }

        #endregion
    }
}
