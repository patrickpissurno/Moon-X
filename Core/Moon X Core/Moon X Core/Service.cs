using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moon_X_Core
{
    public class Service
    {
        public TcpListener Listener;
        public const int Port = 28423;
        private const int BUFFER_SIZE = 1024;
        private string messageQueue = "";
        public void Init()
        {
            Listener = new TcpListener(IPAddress.Parse("127.0.0.1"),Port);
            Listener.Start();

            var buffer = new byte[BUFFER_SIZE];
            while (true)
            {
                TcpClient client = null;
                NetworkStream stream = null;

                try
                {
                    client = Listener.AcceptTcpClient();
                    while (true)
                    {
                        stream = client.GetStream();
                        var bytesReceived = 0;

                        while (true)
                        {
                            bytesReceived = stream.Read(buffer, 0, BUFFER_SIZE);
                            RequestHandler(Encoding.UTF8.GetString(buffer, 0, bytesReceived));
                            if (bytesReceived != BUFFER_SIZE) break;
                        }

                        var response = Encoding.UTF8.GetBytes(messageQueue);
                        Console.WriteLine("\n" + messageQueue + "\n");
                        messageQueue = "";

                        stream.Write(response, 0, response.Length);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n" + e.ToString() + "\n");
                }
                finally
                {
                    stream.Close();
                    client.Close();
                }
            }
        }

        private void RequestHandler(string request)
        {
            string r = request.Trim().ToLowerInvariant();
            r = r.Substring(r.IndexOf("$") + 1, r.LastIndexOf("$") - r.IndexOf("$") - 1);
            Console.WriteLine("\n" + r + "\n");
            switch(r)
            {
                case "installedapps":
                    messageQueue = "{\"type\":\"installedApps\", \"data\":[";
                    foreach (App app in MainForm.instance.InstalledApps)
                    {
                        messageQueue += app.Serialize();
                        if (app != MainForm.instance.InstalledApps[MainForm.instance.InstalledApps.Count - 1])
                            messageQueue += ", ";
                    }
                    messageQueue += "]}";
                    break;
                default:
                    string[] datas = r.Split(';');
                    foreach(string data in datas)
                    {
                        string[] keypair = data.Split('=');
                        switch (keypair[0])
                        {
                            case "runapp":
                                messageQueue = "{\"type\":\"exit\"}";
                                Thread appStart = new Thread(() => {
                                    var process = System.Diagnostics.Process.Start(keypair[1]);
                                    Thread.Sleep(5000);
                                    if (keypair[1].IndexOf("steam") != -1)
                                    {
                                        string id = keypair[1].Substring(keypair[1].LastIndexOf('/') + 1, keypair[1].Length - keypair[1].LastIndexOf('/') - 1);
                                        while ((int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam\Apps\" + id, "Running", 0) != 0)
                                        {
                                            Thread.Sleep(500);
                                        }
                                        
                                    }
                                    else
                                        process.WaitForExit();
                                    MainForm.instance.RestartInterface();
                                });
                                appStart.IsBackground = true;
                                appStart.Start();
                                break;
                            default:
                                messageQueue = "Unknow Service";
                                break;
                        }
                    }
                    if(datas.Length == 0)
                        messageQueue = "Unknow Service";
                    break;
            }
        }
    }
}
