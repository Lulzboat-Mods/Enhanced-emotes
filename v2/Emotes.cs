using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Emotes
{
    public class Emotes : BaseScript
    {
        static string emotesText = string.Empty;
        static StringsModel model;
        static List<Emote> emotes = new List<Emote>();
        static bool isInit = false;

        public Emotes()
        {
            //InitializeEmote();
            /*Task.Run(async () =>
            {
                await InitializeEmote();
            });

            while (isInit) ;
            */
            //Screen.ShowNotification("Init done");

            /*
            menu = new UIMenu("Emotes", "");
            menu.OnItemSelect += OnItemSelect;
            menu.Visible = false;

            foreach (Emote emote in emotes)
            {
                menu.AddItem(new UIMenuItem(emote.Description));
            }
            */

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

        string LoadFile()
        {
            return (Function.Call<string>(Hash.LOAD_RESOURCE_FILE, "emotes", "strings.json"));
            //Screen.ShowNotification(test.Count().ToString());
        }

        void LoadModelFromString(string str)
        {
            Debug.WriteLine("JSON: {0}", str);

            //JavaScriptSerializer serializer = new JavaScriptSerializer();

            //StringsModel myObject = serializer.Deserialize<StringsModel>(str);
            //JsonData data = JsonMapper.ToObject(str);
            //JObject objet = JObject.Parse(str); // CRASH

            //model = serializer.Deserialize<StringsModel>(str);
            //model = JsonConvert.DeserializeObject<StringsModel>(str);
            /*
            foreach (EmoteModel emoteModel in model.emotes)
            {
                emotes.Add(new Emote()
                {
                    EmoteType = emoteModel.hash,
                    Command = emoteModel.title,
                    Description = emoteModel.description.ElementAt(model.language).title,
                    IntValue = 0,
                    BoolValue = emoteModel.boolean
                });
            }

            foreach (Emote emote in emotes)
            {
                if (emotesText == string.Empty)
                    emotesText += emote.Command;
                else
                    emotesText += ", " + emote.Command;
            }
            */
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
                    Screen.ShowNotification(model.errorVehicle.ElementAt(model.language).title);
            }
            else
                Screen.ShowNotification(model.errorPlayerID.ElementAt(model.language).title);
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
                Screen.ShowNotification(Strings.ErrorUnexpected[(int)LanguageEnum.EN]);

            menu.Visible = false;
        }
        */

        void ChatMessage(dynamic _source, dynamic _name, dynamic _message)
        {
            string message = (string)_message;
            string test = string.Empty;

            if (message.StartsWith("/load"))
            {
                test = LoadFile();
                Screen.ShowNotification("Test: " + test.Length);
                LoadModelFromString(test); // 2 times?
            }
            else if (message.StartsWith("/emote"))
                PrintEmoteList();
            else if (message.StartsWith("/cancel"))
                CancelEmote();
            else
            {
                Emote emote = emotes.FirstOrDefault(o => o.Command == message);
                if (emote != null)
                    PlayEmote(emote);
                else
                    Screen.ShowNotification(model.errorBadArgs.ElementAt(model.language).title);
            }
        }

        #endregion
    }
}
