using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotes
{
    public static class Descriptions
    {
        public static readonly string[][] DescriptionStrings =
        {
            new string[] {"Cop", "Policier"},
            new string[] {"Sit", "S'assoir"},
            new string[] {"Kneel", "S'agenouiller"},
            new string[] {"Medic", "Médecin"},
            new string[] {"Notepad", "Bloc-note"},
            new string[] {"Traffic", "Circulation"},
            new string[] {"Photo", "Photo"},
            new string[] {"Clipboard", "Presse-papier"},
            new string[] {"Lean", "S'appuyer"},
            new string[] {"Smoke", "Fumer"},
            new string[] {"Drink", "Boire"}
        };

        public static readonly string[] ErrorStrings =
        {
            "You are in a vehicle",
            "Vous êtes dans un véhicule"
        };
    }
}
