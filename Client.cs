using System;
using System.Net; 
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace SocketTest
{
    public class Client
    {
        private IPAddress endIP;
        private int endPort;
        private IPEndPoint myIPE;
        private Socket mySocket;
        private string testMessage;
        private byte[] encodedMessage;
        private int bytesSent;
        // Data buffer for incoming data.  
        private byte[] bytes;
        public Client()
        {
            endPort = 52525;
            testMessage = "Hello World!<EOF>";
            bytes = new byte[1024];
        }

        public void createSocketAndConnect()
        {
            getSocket();
            getIPE();
            connectToIPE();
        }

        private void getSocket()
        {
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void getIPE()
        {
            myIPE = new IPEndPoint(endIP, endPort);
        }

        private void connectToIPE()
        {
            try
            {
                mySocket.Connect(myIPE);
            }
            catch (ArgumentNullException ae)
            {
                Console.WriteLine("ArgumentNullException : {0}", ae.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        public void sendMessage()
        {
            encodedMessage = Encoding.ASCII.GetBytes(testMessage);
            bytesSent = mySocket.Send(encodedMessage);
            Console.WriteLine(bytesSent);
            // Receive the response from the remote device.  
            int bytesRec = mySocket.Receive(bytes);
            Console.WriteLine("Echoed test = {0}",
                Encoding.ASCII.GetString(bytes, 0, bytesRec));
            // Release the socket.  
            mySocket.Shutdown(SocketShutdown.Both);
            mySocket.Close();
        }
    }
}
