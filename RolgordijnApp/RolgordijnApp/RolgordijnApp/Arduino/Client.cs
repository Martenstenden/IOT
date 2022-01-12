using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;

namespace RolgordijnApp
{
    public class Client
    {
        public static Socket sender;
        public static IPAddress ipAddress = IPAddress.Parse("192.168.69.72");
        public static IPEndPoint remoteEP;
        public static byte[] bytes = new byte[1024];

        public static void OpenConnection()
        {
            remoteEP = new IPEndPoint(ipAddress, 11000); //Gebruik een andere poort als dat nodig is moet dezelfde zijn als op de server
            sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(remoteEP);
            }
            catch
            {

            }
        }

        public static void Send(string message)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            sender.Send(msg);
        }

        public static string Receive()
        {
            while (sender.Available > 0)
            {
                int bytesRec = sender.Receive(bytes);
                return Encoding.ASCII.GetString(bytes, 0, bytesRec);
                //byte[] buf = new byte[1024];
                //string msg;
                //sender.Receive(buf);
                //msg = Encoding.UTF8.GetString(buf);

                //return msg;
            }

            return "Niks ontvangen";
        }
        


        public static void CloseConnection()
        {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
