using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;  // Reference to the Light component
    public float minIntensity = 0.5f;  // Minimum intensity of the light
    public float maxIntensity = 2.0f;  // Maximum intensity of the light
    public float flickerSpeed = 0.1f;  // Speed at which the light flickers
    public float effectDistance = 10f;  // Distance from the light where the effect is applied
    public Transform player;  // Reference to the player or any other target to measure distance from

    private float targetIntensity;

    void Start()
    {
        if (flickeringLight == null)
            flickeringLight = GetComponent<Light>();
    }

    void Update()
    {
        // Calculate the distance from the player to the light
        float distance = Vector3.Distance(player.position, transform.position);

        // If the distance is less than the effect distance, we will apply the flickering effect
        if (distance <= effectDistance)
        {
            // Flicker the light intensity between min and max
            targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0f));
            flickeringLight.enabled = true;  // Make sure the light is on when within range
        }
        else
        {
            // If the player is out of range, turn off the light completely
            flickeringLight.enabled = false;
        }

        // Apply the target intensity to the light if it is enabled
        if (flickeringLight.enabled)
        {
            flickeringLight.intensity = targetIntensity;
        }
    }
}
