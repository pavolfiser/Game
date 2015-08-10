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
            byte[] Databyte = new byte[10024];
            byte[] Messagebyte = new byte[10024];
            Console.WriteLine("Connected to Server");
            int i = 0;
            while (true)
            {
              //  Console.Write("To Sertver: ");
                string message = i.ToString();// Console.ReadLine();
                i++;
           
                networkStream = Client.GetStream();
               
                Messagebyte = ASCIIEncoding.ASCII.GetBytes(message+"$");

                networkStream.Write(Messagebyte, 0, Messagebyte.Length);

                networkStream.Flush();



                Databyte = new byte[10024];

                networkStream.Read(Databyte, 0, Databyte.Length);

                string mess = ASCIIEncoding.ASCII.GetString(Databyte);
                mess = mess.Substring(0, mess.IndexOf("$"));
                Console.Title = mess;
             //   Console.WriteLine("From Server:"+mess);

            }


        }
        }
    }

