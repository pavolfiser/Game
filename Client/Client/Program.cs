using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        
              static void Main(string[] args)
        {
            TcpClient Client = new TcpClient();
            Console.ReadLine();
            Client.Connect("127.0.0.1", 8888);
          
            NetworkStream networkStream;
            byte[] Databyte = new byte[1024];
            byte[] Messagebyte = new byte[1024];
            Console.WriteLine("Connected to Server");

            while (true)
            {
                string message = Console.ReadLine();
                networkStream = Client.GetStream();
                Console.WriteLine(message);
                Messagebyte = ASCIIEncoding.ASCII.GetBytes(message+"$");

                networkStream.Write(Messagebyte, 0, Messagebyte.Length);

                networkStream.Flush();



                Databyte = new byte[1024];

                networkStream.Read(Databyte, 0, Databyte.Length);

                string mess = ASCIIEncoding.ASCII.GetString(Databyte);
                mess = mess.Substring(0, mess.IndexOf("$"));
                Console.WriteLine(mess);

            }


        }
        }
    }

