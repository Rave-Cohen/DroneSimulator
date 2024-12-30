using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPListener : MonoBehaviour
{
    public int port = 1000; // Port to listen on
    private UdpClient udpListener;
    private IPEndPoint remoteEndPoint;

    private bool isListening = true;

    void Start()
    {
        // Initialize the UDP listener
        udpListener = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        Debug.Log($"Listening for UDP messages on port {port}...");

        // Start listening in a background thread
        StartListening();
    }

    private async void StartListening()
    {
        while (isListening)
        {
            try
            {
                // Wait for data asynchronously
                UdpReceiveResult result = await udpListener.ReceiveAsync();
                string receivedMessage = Encoding.ASCII.GetString(result.Buffer);

                Debug.Log($"Received: {receivedMessage} from {result.RemoteEndPoint}");

                // Handle received data (e.g., trigger actions in Unity)
                ProcessReceivedData(receivedMessage);
            }
            catch (Exception ex)
            {
                Debug.LogError($"UDP Listener error: {ex.Message}");
            }
        }
    }

    private void ProcessReceivedData(string data)
    {
        // Example: Log received triggers
        Debug.Log($"Trigger Received: {data}");

        // Add your trigger processing logic here
    }

    private void OnDestroy()
    {
        // Stop the listener when the GameObject is destroyed
        isListening = false;
        udpListener.Close();
        Debug.Log("UDP Listener stopped.");
    }
}
