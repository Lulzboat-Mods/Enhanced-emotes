using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotes
{
    public class StringsModel
    {
        public List<EmoteModel> emotes;
        public List<DescriptionModel> errorVehicle;
        public List<DescriptionModel> errorBadArgs;
        public List<DescriptionModel> errorUnexpected;
        public List<DescriptionModel> errorPlayerID;

        // CONFIG

        public int language = 0;
    }

    public class EmoteModel
    {
        public string title;
        public string hash;
        public bool boolean;
        public List<DescriptionModel> description;
    }

    /*
    public class ErrorModel
    {
        public List<DescriptionModel> description;
    }
    */
    public class DescriptionModel
    {
        public string title;
    }
}
