using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytesFrom = new byte[10024];


            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.Title = "Server is runing";
            

            

            while (true)
            {

                clientSocket = serverSocket.AcceptTcpClient();
                bytesFrom = new byte[10024];
                string datas = "";


                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                
                    Console.WriteLine("Pripojeny z IP: {0}",
                            clientSocket.Client.RemoteEndPoint.ToString());
                 
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                   
              
                    datas = System.Text.Encoding.ASCII.GetString(bytesFrom);

                    datas = datas.Substring(0, datas.IndexOf("$"));
                   


                    ClientHandle client = new ClientHandle(clientSocket);
                    client.Start();

                    bytesFrom = Encoding.ASCII.GetBytes("PROPIJENE & SPOJENUE VYTVORENE$");
                    networkStream.Write(bytesFrom, 0, bytesFrom.Length);
                    networkStream.Flush();

                   

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }


            }
        }
    }
}
