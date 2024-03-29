﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp.发送socket
{
    class SendSocketServer
    {
        public static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            for (int i = 0; i < hostEntry.AddressList.Length; i++)
            {
                try
                {
                    var address = hostEntry.AddressList[i];
                    IPEndPoint ipe = new IPEndPoint(address, port);
                    Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    //tempSocket.Bind(ipe);
                    tempSocket.Connect(ipe);

                    if (tempSocket.Connected)
                    {
                        s = tempSocket;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception e) { }
                
            }

            return s;
        }

        // This method requests the home page content for the specified server.
        public static string SocketSendReceive(string server, int port)
        {
            string request = "GET / HTTP/1.1\r\nHost: " + server +
                "\r\nConnection: Close\r\n\r\n";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];
            string page = "";

            // Create a socket connection with the specified server and port.
            using (Socket s = ConnectSocket(server, port))
            {

                if (s == null)
                    return ("Connection failed");

                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                //int bytes = 0;
                //page = "Default HTML page on " + server + ":\r\n";

                // The following will block until the page is transmitted.
                //do
                //{
                //    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                //    page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                //}
                //while (bytes > 0);
            }

            return page;
        }
    }
    
}
