using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_X_Core
{
    public class Steam
    {
        public static string GetDirectory()
        {
            string[] letters = {"C", "D", "E", "F", "G", "H", "I"};
            string[] paths = {@"Program Files\Steam", @"Program Files (x86)\Steam"};
            foreach(string letter in letters)
            {
                if(Misc.DriveExists(letter + @":\"))
                {
                    foreach (string path in paths)
                    {
                        if (Directory.Exists(letter + @":\" + path))
                        {
                            return letter + @":\" + path;
                        }
                    }
                }
            }
            return null;
        }

        public static App[] GetInstalledGames()
        {
            string path = GetDirectory();
            List<App> apps = new List<App>();
            if (path != null)
            {
                if (Directory.Exists(path + @"\steamapps"))
                {
                    string[] filePaths = Directory.GetFiles(path + @"\steamapps", "*.acf");
                    foreach (string filePath in filePaths)
                    {
                        string fileData = File.ReadAllText(filePath);
                        string id = GetAcfKey(fileData, "appID");
                        App app = new App();
                        app.Path = "steam://" + id;
                        apps.Add(app);
                    }
                }
                else
                    return null;
            }
            return apps.ToArray();
        }

        public static string GetAcfKey(string acf, string key)
        {
            int pos = acf.IndexOf("\"", acf.IndexOf(key) + key.Length + 1);
            return acf.Substring(pos + 1, acf.IndexOf("\"", pos + 1) - pos - 1);
        }
    }
}
