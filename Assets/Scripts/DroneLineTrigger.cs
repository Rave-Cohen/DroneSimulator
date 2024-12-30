using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class DroneLineTrigger : MonoBehaviour
{
    private Socket socket;
    private IPEndPoint endPoint;

    // Initialize the socket
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);

        Debug.Log("Socket initialized. Ready to send triggers.");
    }

    // Detect when the drone crosses a line (using colliders)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line")) // Tag your lines as "Line"
        {
            string trigger = other.gameObject.name; // Use the line's name or custom trigger
            byte[] sendBytes = Encoding.ASCII.GetBytes(trigger);

            // Send the trigger
            socket.SendTo(sendBytes, endPoint);
            Debug.Log($"Trigger sent: {trigger}");
        }
    }

    // Clean up the socket
    private void OnDestroy()
    {
        if (socket != null)
        {
            socket.Close();
            Debug.Log("Socket closed.");
        }
    }
}
