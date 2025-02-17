using UnityEngine;

public class ArrowFlickerManager : MonoBehaviour
{
    public Transform drone;                 // Reference to the drone
    public Transform linesParent;           // Parent of all trigger lines
    public float flickerDistance = 5f;      // Distance before the line to start flickering
    public GameObject leftArrow;            // Reference to the left arrow GameObject
    public GameObject rightArrow;           // Reference to the right arrow GameObject
    public float flickerSpeed = 0.2f;       // Time interval between flickers
    public float[] flickerSequence = new float[24] { -1, 1, -1, -1, 1, 1, -1, 1, 1, 1, -1, -1, -1, 1, -1, 1, -1, 1, -1, 1, -1, 1, -1, 1 };         // Array for the flicker sequence (-1 for left, 1 for right)
    private int sequenceIndex = 0;          // Index to track current flicker step
    private bool isFlickering = false;
    private Transform closestLine = null;   // Current closest line

    void Update()
    {
        // Find the closest line 
        closestLine = GetClosestLine();

        if (closestLine != null)
        {
            float distance = Vector3.Distance(drone.position, closestLine.position);

            if (distance <= flickerDistance && !isFlickering)
            {
                StartFlickering();
            }
            else if (distance > flickerDistance && isFlickering)
            {
                StopFlickering();
            }
        }
    }

    private Transform GetClosestLine()
    {
        Transform closest = null;
        float minDistance = float.MaxValue;

        foreach (Transform line in linesParent) // Iterate through all child lines
        {
            float distance = Vector3.Distance(drone.position, line.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = line;
            }
        }

        return closest;
    }

    private void StartFlickering()
    {
        isFlickering = true;
        sequenceIndex = 0;  // Reset sequence when flickering starts
        StartCoroutine(FlickerArrows());
    }

    private void StopFlickering()
    {
        isFlickering = false;
        StopAllCoroutines();  // Stop the flicker coroutine
        leftArrow.SetActive(false);  // Turn off the left arrow
        rightArrow.SetActive(false); // Turn off the right arrow
    }

    private System.Collections.IEnumerator FlickerArrows()
    {
        while (isFlickering)
        {
            if (sequenceIndex >= flickerSequence.Length)
            {
                sequenceIndex = 0; // Loop back to the start of the sequence
            }

            // Determine which arrow to flicker based on the array value
            if (flickerSequence[sequenceIndex] == -1)
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(false);
            }
            else if (flickerSequence[sequenceIndex] == 1)
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(true);
            }

            yield return new WaitForSeconds(flickerSpeed);

            // Turn off both arrows after flicker
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);

            yield return new WaitForSeconds(flickerSpeed); // Wait before the next flicker

            sequenceIndex++; // Move to the next step in the sequence
        }
    }

    private void OnDrawGizmos()
    {
        if (linesParent != null)
        {
            Gizmos.color = Color.red;
            foreach (Transform line in linesParent)
            {
                Gizmos.DrawWireSphere(line.position, flickerDistance); // Draw flicker distance for each line
            }
        }
    }
}
