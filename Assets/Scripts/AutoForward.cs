using System.Diagnostics.Tracing;
using PA_DronePack;
using UnityEngine;

public class AutoForward : MonoBehaviour
{
    private bool isAutoForward = false;
    private PA_DroneAxisInput droneInput;
    private PA_DroneController droneController;

    [Header("Strafe Settings")]
    public float StrafeDuration = 2f; // Duration of strafe in seconds
    public float StrafeSpeed = 0.5f; // Adjust this value to control strafe speed
    private bool isStrafingLeft = false;
    private bool isStrafingRight = false;
    private float strafeTimeRemaining = 0f;
    //fields to define auto strafe
    private float[] strafeArr = new float[25] {-1, 1, -1, -1, 1, 1, -1, 1, 1, 1,-1, -1, -1, 1, -1, 1, -1, 1 , -1, 1, -1 , 1, -1, 1, -1};
    private int counter = 0;
    //built the enviornment that fit strafe duration = 2 and strafe speed = 0.5
    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            droneInput = player.GetComponent<PA_DroneAxisInput>();
            droneController = player.GetComponent<PA_DroneController>();
        }

        if (droneInput == null || droneController == null)
        {
            Debug.LogError("Missing PA_DroneAxisInput or PA_DroneController component!");
        }
    }

    void Update()
    {
        if (droneInput == null || droneController == null) return;

        // Toggle auto-forward on Spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAutoForward = !isAutoForward;
            Debug.Log("Auto-forward toggled: " + isAutoForward);
        }

        if (isAutoForward)
        {
            droneController.DriveInput(1f);
        }
        else
        {
            float axisInput = Input.GetAxisRaw(droneInput.forwardBackward);
            droneController.DriveInput(axisInput);
        }

        // Start strafing left
        if (Input.GetKeyDown(KeyCode.V) && !isStrafingLeft)
        {
            StartStrafing(-1f);
        }

        // Start strafing right
        if (Input.GetKeyDown(KeyCode.B) && !isStrafingRight)
        {
            StartStrafing(1f);
        }

        // Handle ongoing strafing
        if (isStrafingLeft || isStrafingRight)
        {
            if (strafeTimeRemaining > 0f)
            {
                strafeTimeRemaining -= Time.deltaTime;
                // Apply continuous strafing
                droneController.StrafeInput(isStrafingLeft ? -StrafeSpeed : StrafeSpeed);
            }
            else
            {
                StopStrafing();
            }
        }
    }

    private void StartStrafing(float direction)
    {
        if (direction < 0) isStrafingLeft = true;
        if (direction > 0) isStrafingRight = true;

        strafeTimeRemaining = StrafeDuration; // Reset timer
        Debug.Log((direction < 0 ? "Left" : "Right") + " strafing started for " + StrafeDuration + " seconds.");
    }

    private void StopStrafing()
    {
        isStrafingLeft = false;
        isStrafingRight = false;
        strafeTimeRemaining = 0f;
        droneController.StrafeInput(0f); // Stop strafing
        Debug.Log("Strafing stopped.");
    }

    void OnTriggerEnter(Collider other)
    {
        StartStrafing(strafeArr[counter]);
        counter++;
    }
}
