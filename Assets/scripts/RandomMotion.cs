using UnityEngine;

public class BoundedMotion : MonoBehaviour
{
    public float minForce = 0.1f; // Minimum force magnitude
    public float maxForce = 0.5f; // Maximum force magnitude
    public float interval = 0.5f; // Time interval between applying forces
    public float sphereRadius = 1f; // Radius of the bounding sphere

    private Rigidbody rb; // Reference to the Rigidbody component
    private Vector3 originalLocalPosition; // Original local position relative to the parent
    private Transform parentTransform; // Reference to the parent object's transform

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();

        // Save the original local position relative to the parent
        originalLocalPosition = transform.localPosition;

        // Get the parent object's transform
        parentTransform = transform.parent;

        // Start applying random forces periodically
        InvokeRepeating("ApplyRandomForce", 0f, interval);
    }

    void ApplyRandomForce()
    {
        // Generate a random direction for the force
        Vector3 randomDirection = Random.insideUnitSphere;

        // Generate a random force magnitude
        float randomForceMagnitude = Random.Range(minForce, maxForce);

        // Apply the force to the object's Rigidbody component
        rb.AddForce(randomDirection * randomForceMagnitude, ForceMode.Impulse);

        // Ensure the object stays within the bounding sphere
        Vector3 newPosition = transform.localPosition + rb.velocity * Time.deltaTime;
        newPosition = Vector3.ClampMagnitude(newPosition, sphereRadius);

        // Update the local position relative to the parent
        transform.localPosition = newPosition;

        // Keep the spawned object's position relative to the parent object
        transform.position = parentTransform.TransformPoint(originalLocalPosition);
    }

    void OnDrawGizmosSelected()
    {
        // Draw the bounding sphere in the Scene view for visualization
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
