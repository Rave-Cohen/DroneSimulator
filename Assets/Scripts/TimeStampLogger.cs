using UnityEngine;
using System.IO;

public class TimeStampLogger : MonoBehaviour
{
    public string lineName; // Name of the line (e.g., "Line1", "Line2")

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            long unixTime = GetUnixTime();
            string logMessage = $"[{unixTime}] {lineName} crossed by {other.name}\n";

            // Append the message to a file
            File.AppendAllText("TimestampsLog.txt", logMessage);

            // Print a short message to the Unity Console
            Debug.Log($"{lineName} crossed by {other.name} at {unixTime}");
        }
    }

    // Get the current Unix timestamp
    private long GetUnixTime()
    {
        return new System.DateTimeOffset(System.DateTime.UtcNow).ToUnixTimeSeconds();
    }
}
