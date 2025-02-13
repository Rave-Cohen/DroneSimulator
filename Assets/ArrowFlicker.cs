using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ArrowFlicker : MonoBehaviour
{
    public float distanceThreshold = 10f; // Maximum distance for the arrow to appear
    public string rockTag = "Rock"; // Tag to identify rocks
    public float[] flickerSequence = new float[24]
    {
        -1, 1, -1, -1, 1, 1, -1, 1,
        1, 1, -1, -1, -1, 1, -1, 1,
        -1, 1, -1, 1, -1, 1, -1, 1
    }; // Array for flicker sequence

    public GameObject leftArrow; // Assign the "Arrow(1)" GameObject
    public GameObject rightArrow; // Assign the "Arrow" GameObject
    public float flickerDuration = 4f; // Total flicker duration for the sequence

    private HashSet<string> flickeredRocks = new HashSet<string>(); // Tracks names of rocks that flickered
    private int flickerIndex = 0; // Tracks the current position in the flicker sequence
    private bool isFlickering = false; // Tracks if a flicker is currently happening

    private float direction;

    private GameObject[] rocks;

    void Start()
    {
        // Start with both arrows off
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        rocks = GameObject.FindGameObjectsWithTag(rockTag);

        direction = 0;

    }

    void Update()
    {
        if (!isFlickering)
        {
            foreach (GameObject rock in rocks)
            {
                float distance = Vector3.Distance(transform.position, rock.transform.position);

                // If rock is within range and hasn't flickered yet
                if (distance <= distanceThreshold && !flickeredRocks.Contains(rock.name))
                {
                    flickeredRocks.Add(rock.name); // Mark this rock as flickered
                    FlickerArrows();
                    break; // Only start one flicker at a time
                }
            }
        }
    }

    // Start the flickering process
    private System.Collections.IEnumerator FlickerArrows()
    {
        isFlickering = true;
        
        direction = flickerSequence[flickerIndex];

        leftArrow.SetActive(direction == -1);
        rightArrow.SetActive(direction == 1);

        yield return new WaitForSeconds(flickerDuration);

        flickerIndex++;

        // Turn off both arrows at the end
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

        isFlickering = false;
    }

    void OnDrawGizmos()
    {
        // Visualize the distance threshold
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceThreshold);
    }
}
