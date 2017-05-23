using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Threading.Tasks;

namespace Emotes
{
    public class Emotes : BaseScript
    {
        //UIMenu menu;

        public Emotes()
        {
            EventHandlers["printEmoteList"] += new Action(PrintEmoteList);
            EventHandlers["playEmote"] += new Action<dynamic>(PlayEmote);
            EventHandlers["cancelEmote"] += new Action(CancelEmote);
            
            /*
            menu = new UIMenu("test", "test");
            UIMenuItem item = new UIMenuItem("lol");

            menu.AddItem(item);
            menu.Visible = true;
            menu.OnItemSelect += OnItemSelect;
            */
            Tick += OnTick;
        }

        /*
        private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            Screen.ShowNotification(selectedItem.Text);
        }
        */
        public async Task OnTick()
        {
            // Show notification when Z was pressed
            if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
            {
                //Screen.ShowNotification("Hello, mister!");
                CancelEmote();
            }
        }
        
        #region Events

        void PrintEmoteList()
        {
            TriggerEvent("chatMessage", "ALERT", new int[] { 255, 0, 0 } , "Emote List: cop, sit, chair, kneel, medic, notepad, traffic, photo, clipboard, lean, smoke, drink");
        }

        void CancelEmote()
        {
            Function.Call(Hash.CLEAR_PED_TASKS, Game.PlayerPed);
        }

        void PlayEmote(dynamic arg)
        {
            string emoteName = (string)arg;

            Screen.ShowNotification(emoteName);

            if (Game.PlayerPed != null)
            {
                Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, Game.PlayerPed, emoteName, 0, true);
            }
        }

        #endregion
    }
}
