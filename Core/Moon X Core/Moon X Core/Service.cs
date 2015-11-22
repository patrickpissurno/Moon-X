using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
                    stream = client.GetStream();
                    var bytesReceived = 0;

                    while (true)
                    {
                        bytesReceived = stream.Read(buffer, 0, BUFFER_SIZE);
                        RequestHandler(Encoding.ASCII.GetString(buffer, 0, bytesReceived));
                        if (bytesReceived != BUFFER_SIZE) break;
                    }

                    var response = Encoding.ASCII.GetBytes(messageQueue);
                    messageQueue = "";

                    stream.Write(response, 0, response.Length);
                }
                catch (Exception e)
                {
                    MainForm.instance.ShowMessage(e.ToString());
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
            if (r.Equals("installedapps"))
            {
                foreach (App app in MainForm.instance.InstalledApps)
                {
                    messageQueue += app.Serialize();
                }
            }
            else
                messageQueue = "Unknow Service";
            //MainForm.instance.ShowMessage(request);
        }
    }
}
