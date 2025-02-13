//using System.Diagnostics.Tracing;
//using PA_DronePack;
//using UnityEngine;

//public class AutoForward : MonoBehaviour
//{
//    private bool isAutoForward = false;
//    private PA_DroneAxisInput droneInput;
//    private PA_DroneController droneController;

//    [Header("Strafe Settings")]
//    public float StrafeDuration = 2f; // Duration of strafe in seconds
//    public float StrafeSpeed = 0.5f; // Adjust this value to control strafe speed
//    private bool isStrafingLeft = false;
//    private bool isStrafingRight = false;
//    private float strafeTimeRemaining = 0f;
//    //fields to define auto strafe
//    private float[] strafeArr = new float[24] {-1, 1, -1, -1, 1, 1, -1, 1, 1, 1,-1, -1, -1, 1, -1, 1, -1, 1 , -1, 1, -1 , 1, -1, 1};
//    private int counter = 0;
//    //built the enviornment that fit strafe duration = 2 and strafe speed = 0.5
//    void Start()
//    {

//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            droneInput = player.GetComponent<PA_DroneAxisInput>();
//            droneController = player.GetComponent<PA_DroneController>();
//        }

//        if (droneInput == null || droneController == null)
//        {
//            Debug.LogError("Missing PA_DroneAxisInput or PA_DroneController component!");
//        }
//    }

//    void Update()
//    {
//        if (droneInput == null || droneController == null) return;

//        // Toggle auto-forward on Spacebar press
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            isAutoForward = !isAutoForward;
//            Debug.Log("Auto-forward toggled: " + isAutoForward);
//        }

//        if (isAutoForward)
//        {
//            droneController.DriveInput(1f);
//        }
//        else
//        {
//            float axisInput = Input.GetAxisRaw(droneInput.forwardBackward);
//            droneController.DriveInput(axisInput);
//        }

//        // Start strafing left
//        if (Input.GetKeyDown(KeyCode.V) && !isStrafingLeft)
//        {
//            StartStrafing(-1f);
//        }

//        // Start strafing right
//        if (Input.GetKeyDown(KeyCode.B) && !isStrafingRight)
//        {
//            StartStrafing(1f);
//        }

//        // Handle ongoing strafing
//        if (isStrafingLeft || isStrafingRight)
//        {
//            if (strafeTimeRemaining > 0f)
//            {
//                strafeTimeRemaining -= Time.deltaTime;
//                // Apply continuous strafing
//                droneController.StrafeInput(isStrafingLeft ? -StrafeSpeed : StrafeSpeed);
//            }
//            else
//            {
//                StopStrafing();
//            }
//        }
//    }

//    private void StartStrafing(float direction)
//    {
//        if (direction < 0) isStrafingLeft = true;
//        if (direction > 0) isStrafingRight = true;

//        strafeTimeRemaining = StrafeDuration; // Reset timer
//        Debug.Log((direction < 0 ? "Left" : "Right") + " strafing started for " + StrafeDuration + " seconds.");
//    }

//    private void StopStrafing()
//    {
//        isStrafingLeft = false;
//        isStrafingRight = false;
//        strafeTimeRemaining = 0f;
//        droneController.StrafeInput(0f); // Stop strafing
//        //Debug.Log("Strafing stopped.");
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        StartStrafing(strafeArr[counter]);
//        counter++;
//    }
//}

using System.Collections;
using UnityEngine;
using PA_DronePack;

public class AutoForward : MonoBehaviour
{
    private bool isAutoForward = false;
    private PA_DroneAxisInput droneInput;
    private PA_DroneController droneController;

    [Header("Auto-Forward Settings")]
    // Auto-forward toggled via Spacebar (driving forward)

    [Header("Manual Strafe Settings")]
    public float StrafeDuration = 2f;       // Duration for key-based (manual) strafing
    public float StrafeSpeed = 0.5f;          // Speed for manual strafing
    private bool isStrafingLeft = false;
    private bool isStrafingRight = false;
    private float manualStrafeTimeRemaining = 0f;

    [Header("Triggered Strafe Settings")]
    public float TriggeredStrafeDelay = 2f;      // Time delay before auto strafe begins after trigger (arrow appears for this time)
    public float TriggeredStrafeDuration = 2f;   // Duration of auto strafe once it starts
    private bool isTriggeredStrafeActive = false;

    [Header("Triggered Sequence Settings")]
    // Array of values (-1 for left, 1 for right) defining the trigger sequence.
    private float[] strafeArr = new float[24]
        {-1, 1, -1, -1, 1, 1, -1, 1, 1, 1, -1, -1, -1, 1, -1, 1, -1, 1, -1, 1, -1, 1, -1, 1};
    private int sequenceCounter = 0;

    [Header("Arrow Settings")]
    public GameObject leftArrow;   // Arrow(1) – used for left indication
    public GameObject rightArrow;  // Arrow – used for right indication

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            droneInput = player.GetComponent<PA_DroneAxisInput>();
            droneController = player.GetComponent<PA_DroneController>();
            Debug.Log("Found player and components");
        }
        else
        {
            Debug.LogError("Player not found!");
        }
        if (droneInput == null || droneController == null)
        {
            Debug.LogError("Missing PA_DroneAxisInput or PA_DroneController component!");
        }
    }

    void Update()
    {
        if (droneInput == null || droneController == null) return;

        // ----- Auto-Forward (driving forward) -----
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

        // ----- Manual Key-Based Strafing (if no triggered strafe is active) -----
        if (!isTriggeredStrafeActive)
        {
            if (Input.GetKeyDown(KeyCode.V) && !isStrafingLeft)
            {
                StartManualStrafe(-1f);
            }
            if (Input.GetKeyDown(KeyCode.B) && !isStrafingRight)
            {
                StartManualStrafe(1f);
            }
            if (isStrafingLeft || isStrafingRight)
            {
                if (manualStrafeTimeRemaining > 0f)
                {
                    manualStrafeTimeRemaining -= Time.deltaTime;
                    droneController.StrafeInput(isStrafingLeft ? -StrafeSpeed : StrafeSpeed);
                }
                else
                {
                    StopManualStrafe();
                }
            }
        }
    }

    // Manual strafing functions
    private void StartManualStrafe(float direction)
    {
        if (direction < 0)
        {
            isStrafingLeft = true;
            isStrafingRight = false;
        }
        else
        {
            isStrafingRight = true;
            isStrafingLeft = false;
        }
        manualStrafeTimeRemaining = StrafeDuration;
        Debug.Log((direction < 0 ? "Left" : "Right") + " manual strafing started for " + StrafeDuration + " seconds.");
    }

    private void StopManualStrafe()
    {
        isStrafingLeft = false;
        isStrafingRight = false;
        manualStrafeTimeRemaining = 0f;
        droneController.StrafeInput(0f);
        Debug.Log("Manual strafing stopped.");
    }

    // ----- Triggered Auto Strafe -----
    // When the drone enters a trigger, this coroutine handles arrow appearance and auto strafing.
    void OnTriggerEnter(Collider other)
    {
        // Optionally, filter by tag: if(other.CompareTag("TriggerLine"))
        StartCoroutine(HandleTriggeredStrafe());
    }

    private IEnumerator HandleTriggeredStrafe()
    {
        // Get next direction from the sequence array.
        float direction = strafeArr[sequenceCounter];
        sequenceCounter = (sequenceCounter + 1) % strafeArr.Length;

        // Activate the appropriate arrow immediately.
        if (direction == -1)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
        else if (direction == 1)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        Debug.Log("Triggered: Arrow activated. Waiting " + TriggeredStrafeDelay + " sec before auto strafe.");

        // Wait for the delay (arrow visible before strafe starts).
        yield return new WaitForSeconds(TriggeredStrafeDelay);

        // Start triggered auto strafe.
        isTriggeredStrafeActive = true;
        if (direction < 0)
        {
            isStrafingLeft = true;
            isStrafingRight = false;
        }
        else if (direction > 0)
        {
            isStrafingRight = true;
            isStrafingLeft = false;
        }
        float triggeredTimeRemaining = TriggeredStrafeDuration;
        Debug.Log("Triggered auto strafe started for " + TriggeredStrafeDuration + " sec.");
        while (triggeredTimeRemaining > 0f)
        {
            triggeredTimeRemaining -= Time.deltaTime;
            // Use the given direction multiplied by StrafeSpeed.
            droneController.StrafeInput(direction * StrafeSpeed);
            yield return null;
        }

        isTriggeredStrafeActive = false;
        Debug.Log("Triggered auto strafe ended.");

        // Turn off arrows.
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }
}

