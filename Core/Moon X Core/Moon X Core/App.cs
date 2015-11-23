using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_X_Core
{
    public class App
    {
        public string Name;
        public string Path;
        public string ImagePath;
        public string SteamId = null;

        public string Serialize()
        {
            string result = "{\"name\":\"" + Name + "\", \"path\":\"" + Path + "\", \"imagePath\":\"" + ImagePath + "\"";
            result += SteamId == null ? "}" : ", \"steamId\":\"" + SteamId + "\"}";
            return result;
        }
    }
}
