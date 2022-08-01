using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Soc
{
    class Program
    {

        public static string connectionIP = "127.0.0.1"; //We set the local ip address as the ip address
        public static int connectionPort = 25001; //We wrote the port we want to use
        public static IPAddress localAdd;
        public static TcpListener listener;
        public static TcpClient client;
        public static  bool running;

        static void Main(string[] args)
        {
            Thread mThread;
            ThreadStart ts = new ThreadStart(GetInfo); //We created a thread
            mThread = new Thread(ts);
            mThread.Start(); //We started a thread

        }

        public static void GetInfo()
        {
            
            localAdd = IPAddress.Parse(connectionIP);
            listener = new TcpListener(IPAddress.Any, connectionPort);
            listener.Start(); 

            client = listener.AcceptTcpClient(); //We started listening until the data came
            running = true; //We made the "running variable" true when the data came in.

            while (running)
            {
                SendAndReceiveData(); 
            }
            listener.Stop();
            
        }

        public static void SendAndReceiveData()
        {
       

            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];



            while (true) 
            {
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //We have captured the incoming data
                if (dataReceived != null)
                {
                   
                    Console.WriteLine("incoming data: " + dataReceived); //incoming data
                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes("I got your Python message. Can you see my message?"); 
                    nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //We sent a reply

                }
               
            }
 
        }
    }
}
