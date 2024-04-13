using UnityEngine;

public class MoveToSphere : MonoBehaviour
{
    public GameObject sphereObject; // Reference to the sphere GameObject
    public float moveSpeed = 1f; // The speed of movement
    public float minForce = 0.1f; // Minimum force magnitude for random motion
    public float maxForce = 0.5f; // Maximum force magnitude for random motion

    private float sphereRadius; // The radius of the sphere

    void Start()
    {
        // Get the radius of the sphere
        sphereRadius = sphereObject.GetComponent<SphereCollider>().radius * sphereObject.transform.localScale.x;
    }

    void Update()
    {
        // Calculate the direction from the object's current position to the center of the sphere
        Vector3 directionToSphereCenter = (sphereObject.transform.position - transform.position).normalized;

        // Calculate the distance from the object's current position to the surface of the sphere
        float distanceToSphereSurface = Vector3.Distance(transform.position, sphereObject.transform.position) - sphereRadius;

        // If the object is outside the sphere, move it towards the sphere's surface
        if (distanceToSphereSurface > 0.01f)
        {
            // Move the object towards the surface of the sphere
            transform.position += directionToSphereCenter * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Apply random forces to the object to simulate random motion inside the sphere
            Vector3 randomForce = Random.insideUnitSphere.normalized * Random.Range(minForce, maxForce);
            GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);
        }
    }
}
