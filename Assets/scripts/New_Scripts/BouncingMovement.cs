using UnityEngine;

public class BouncingMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject cylinderObject;

    private Transform cylinderTransform;
    private float cylinderRadius;

    private Vector3 direction;
    private bool initialReflectionOccurred = false;

    void Start()
    {
        // Get the transform of the cylinder object
        cylinderTransform = cylinderObject.transform;

        // Calculate the radius of the cylinder
        cylinderRadius = cylinderTransform.localScale.x / 2f;

        // Calculate initial movement direction towards the curved sides of the cylinder
        Vector3 closestPointOnCylinder = cylinderTransform.position + (transform.position - cylinderTransform.position).normalized * cylinderRadius;
        direction = (closestPointOnCylinder - transform.position).normalized;

        // Set initial position inside the cylinder
        transform.position = cylinderTransform.position + direction * (cylinderRadius - 0.1f); // Subtracting 0.1f for safety margin
    }

    void Update()
    {
        // Move the object
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the object reaches the sides of the cylinder
        if (!initialReflectionOccurred &&
            (Mathf.Abs(transform.position.x - cylinderTransform.position.x) > cylinderRadius ||
            Mathf.Abs(transform.position.z - cylinderTransform.position.z) > cylinderRadius))
        {
            // Reflect the direction
            direction = -direction;
            initialReflectionOccurred = true; // Set the flag
        }
    }
}
