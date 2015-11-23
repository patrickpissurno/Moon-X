using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    string imageDirectory = Path.Combine(Application.StartupPath, @"Resources\BoxArt\");
                    Directory.CreateDirectory(imageDirectory);
                    string[] filePaths = Directory.GetFiles(path + @"\steamapps", "*.acf");

                    foreach (string filePath in filePaths)
                    {
                        string fileData = File.ReadAllText(filePath);
                        string id = GetAcfKey(fileData, "appID");
                        App app = new App();
                        app.Path = "steam://rungameid/" + id;
                        app.Name = GetAcfKey(fileData, "name");
                        app.ImagePath = "http://cdn.akamai.steamstatic.com/steam/apps/" + id + "/header.jpg";
                        try
                        {
                            using (WebClient webClient = new WebClient())
                            {
                                string targetPath = Path.Combine(imageDirectory, id + ".jpg");
                                if(!File.Exists(targetPath))
                                    webClient.DownloadFile(app.ImagePath, targetPath);
                                app.ImagePath = targetPath;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nError downloading app image:\n" + ex.Message + "\n");
                        }
                        
                        app.SteamId = id;
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
