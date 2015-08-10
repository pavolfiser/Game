using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Server
{
    class ClientHandle
    {


        private TcpClient ClientSocket;
        private int ID;

        string message = "";


         public ClientHandle(TcpClient clientSocket)
        {

            this.ClientSocket = clientSocket;
          
         

        }
        Thread Vlakno;

        public void Start()
        {

            Vlakno = new Thread(Communication);
            Vlakno.Start();
        }



        void Communication()
        {

            message = Recive(ClientSocket);
            Console.WriteLine(message);
            Send(ClientSocket,message);

        }

        private string Recive(TcpClient clientSocket)
        {
          
            byte[] bytesFrom = new byte[1024];

            NetworkStream networkStream = clientSocket.GetStream();

            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
            string messageFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

            return messageFromClient.Substring(0, messageFromClient.IndexOf("$"));
        }

        private void Send(TcpClient clientSocket,string messageForClient)
        {
            byte[] bytesFrom = new byte[1024];
            NetworkStream networkStream = clientSocket.GetStream();
            bytesFrom = Encoding.ASCII.GetBytes(messageForClient + "$");
            networkStream.Write(bytesFrom, 0, bytesFrom.Length);
            networkStream.Flush();
        }
        

    }
}
