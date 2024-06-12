using UnityEngine;
using Vuforia;

public class VuforiaCameraControl : MonoBehaviour
{
    public float zoomSpeed = 10f;
    private float initialFov;
    private Camera arCamera;

    void Start()
    {
        // Find the AR Camera
        arCamera = Camera.main;

        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found.");
            return;
        }

        // Store the initial field of view
        initialFov = arCamera.fieldOfView;
    }

    void Update()
    {
        // Check for pinch gesture
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calculate the distance between the two touches in the current frame
            float currentTouchDistance = Vector2.Distance(touch1.position, touch2.position);

            // Calculate the difference in distances between the current frame and the previous frame
            float previousTouchDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
            float deltaDistance = currentTouchDistance - previousTouchDistance;

            // Adjust the zoom level based on the distance change
            AdjustZoom(deltaDistance);
        }
    }

    private void AdjustZoom(float deltaDistance)
    {
        // Adjust the camera's field of view to simulate zoom
        float newFov = Mathf.Clamp(arCamera.fieldOfView - deltaDistance * zoomSpeed, 15f, initialFov);
        arCamera.fieldOfView = newFov;
    }
}
