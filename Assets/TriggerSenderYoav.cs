using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace UnicornRecorderUDPTrigger
{
    public class TriggerSenderYoav : MonoBehaviour
    {
        private Socket socket;
        private IPEndPoint endPoint;

        void Start()
        {
            try
            {
                Console.WriteLine("IP (e.g. 127.0.0.1):\n--------------------");
                string ipString = Console.ReadLine();
                IPAddress ip = IPAddress.Parse(ipString);

                Console.WriteLine("Port (e.g. 1000):\n--------------------");
                string portString = Console.ReadLine();
                int port = int.Parse(portString);

                // Initialize socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.EnableBroadcast = true;
                endPoint = new IPEndPoint(ip, port);
                socket.Connect(endPoint);

                Console.WriteLine("Sending triggers.\nPress ENTER key to terminate the application.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not sended", ex.Message, ex.StackTrace);
            }
          

        }
        void Run()
        {
            try 
            {

                new Thread(() =>
                {
                    //Send trigger
                    string trigString = (1).ToString();
                    byte[] sendBytes = Encoding.ASCII.GetBytes(trigString);
                    socket.SendTo(sendBytes, endPoint);
                    Console.WriteLine("Sent trigger with value {1}", trigString);
                    Thread.Sleep(1);
                }).Start();

                Console.ReadLine();
            }
       
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send triggers.\n{0}\n{1}\n", ex.Message, ex.StackTrace);
            }

        }

        void OnTriggerEnter(Collider other)
        {
            // Check if the Player crosses the line
            if (other.CompareTag("Player"))
            {
                Run();
            }
        }

    }

}
