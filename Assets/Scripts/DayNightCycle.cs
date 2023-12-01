using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 10.0f; // Duration of one day in seconds
    public Color dayColor = new Color(0.53f, 0.81f, 0.98f); // Daytime color
    public Color nightColor = new Color(0.0f, 0.0f, 0.1f); // Nighttime color

    public Transform sunMoonTransform; // Drag the sun/moon GameObject here

    private Light directionalLight;
    private Camera mainCamera;

    private void Start()
    {
        directionalLight = GetComponent<Light>();
        mainCamera = Camera.main;

        // Parent the sun/moon GameObject to the camera
        sunMoonTransform.parent = mainCamera.transform;
    }

    private void Update()
    {
        // Calculate the angle to rotate based on time of day, but slower (multiplied by 0.5)
        float angle = Mathf.Repeat(Time.time / (dayDuration * 2), 1.0f) * 360.0f;

        // Rotate the sun/moon object along the Z-axis in a circle
        sunMoonTransform.eulerAngles = -new Vector3(0f, 0f, angle);

        // Adjust the intensity of the directional light in the opposite way
        directionalLight.intensity = Mathf.Lerp(1.0f, 0.3f, Mathf.PingPong(Time.time / dayDuration, 1.0f));

        // Interpolate between day and night colors based on time of day
        mainCamera.backgroundColor = Color.Lerp(dayColor, nightColor, Mathf.PingPong(Time.time / dayDuration, 1.0f));
    }
}
