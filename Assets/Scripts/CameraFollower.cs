using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -10);
    public float smoothSpeed = 0.125f;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Drone").transform;
            if (target == null)
            {
                Debug.LogError("Drone not found!");
            }
        }
    }

    void LateUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Calculate the desired rotation, pointing towards the target
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the rotation to the camera
        transform.rotation = rotation;
    }
}