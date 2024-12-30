using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TriggerSender : MonoBehaviour
{
    private Socket socket;
    private IPEndPoint endPoint;

    void Start()
    {
        // Initialize socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.EnableBroadcast = true;
        endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the Player crosses the line
        if (other.CompareTag("Player"))
        {
            SendTrigger();
        }
    }

    void SendTrigger()
    {
        try
        {
            // Send trigger
            byte[] sendBytes = Encoding.ASCII.GetBytes("0");
            socket.SendTo(sendBytes, endPoint);
            Debug.Log("Trigger sent.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error sending trigger: {ex.Message}");
        }
    }

    void OnDestroy()
    {
        // Cleanup
        if (socket != null)
        {
            socket.Close();
        }
    }
}
