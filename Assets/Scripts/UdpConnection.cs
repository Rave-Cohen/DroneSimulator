using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
//using UnityEditor.PackageManager;
//using UnityEditor.VersionControl;
using UnityEngine;

public class UdpConnection : MonoBehaviour
{
    private UdpClient udpServer;
    private IPEndPoint clientEndPoint;

    public string clientIP = "127.0.0.1"; // Replace with your server IP
    public int clientPort = 1000;
    public int serverPort = 1100;
    void Start()
    {

        try
        {//initializing socket
            udpServer = new UdpClient(serverPort);
            clientEndPoint = new IPEndPoint(IPAddress.Parse(clientIP), clientPort);
            Debug.Log($"UDP server started on port {serverPort}. Sending data to {clientIP}:{clientPort}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error starting UDP server: {ex.Message}");
        }
    }

    void SendMessage()
    {
        try
        {//function to send trigger to unicorn recorder. sending 1 makes a mark
            string message = "1";
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            Debug.Log($"sended message in bytes: {sendBytes}");
            udpServer.Send(sendBytes, sendBytes.Length, clientEndPoint);
            Debug.Log($"Sent message to {clientEndPoint}: {message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error sending message: {ex.Message}");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        SendMessage();
    }

    void OnApplicationQuit()
    {
        StopServer();
    }

    void StopServer()
    {
        udpServer?.Close();
        udpServer = null;
        Debug.Log("UDP server stopped.");
    }
}
