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
        try
        {
            // Initialize socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.EnableBroadcast = true;
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);

            // Check if the socket is valid and "connected"
            if (socket != null)
            {
                socket.Connect(endPoint);
                if (socket.Connected)
                {
                    Debug.Log("Socket successfully connected.");
                }
                else
                {
                    Debug.LogWarning("Socket failed to connect.");
                }
            }
            else
            {
                Debug.LogError("Socket is null.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error initializing socket: {ex.Message}");
        }
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
            if (socket != null && socket.Connected)
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes("0");
                socket.SendTo(sendBytes, endPoint);
                Debug.Log("Trigger sent.");
            }
            else
            {
                Debug.LogWarning("Socket is not connected or is null.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error sending trigger: {ex.Message}");
        }
    }

    void Update()
    {
        // Example of checking if the socket is still usable by sending a test message
        if (socket != null && socket.Connected)
        {
            try
            {
                byte[] testMessage = Encoding.ASCII.GetBytes("Test");
                socket.Send(testMessage);
            }
            catch (SocketException se)
            {
                Debug.LogError($"Socket error: {se.Message}");
            }
        }
        else
        {
            Debug.LogWarning("Socket is not connected or is null.");
        }
    }

    void OnDestroy()
    {
        // Cleanup
        if (socket != null)
        {
            socket.Close();
            Debug.Log("Socket closed.");
        }
    }
}
