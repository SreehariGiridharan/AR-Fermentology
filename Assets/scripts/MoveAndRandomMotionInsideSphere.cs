using UnityEngine;

public class MoveAndRandomMotionInsideSphere : MonoBehaviour
{
    public GameObject sphereObject; // Reference to the sphere GameObject
    public float moveSpeed = 1f; // The speed of movement towards the sphere
    public float minForce = 0.1f; // Minimum force magnitude for random motion inside the sphere
    public float maxForce = 0.5f; // Maximum force magnitude for random motion inside the sphere

    private float sphereRadius; // The radius of the sphere
    private bool reachedSphere = false; // Flag to indicate if the object has reached the sphere's surface

    void Start()
    {
        // Get the radius of the sphere
        sphereRadius = sphereObject.GetComponent<SphereCollider>().radius * sphereObject.transform.localScale.x;
    }

    void Update()
    {
        if (!reachedSphere)
        {
            MoveTowardsSphere();
        }
        else
        {
            MoveRandomlyInsideSphere();
        }
    }

    void MoveTowardsSphere()
    {
        // Calculate the direction from the object's current position to the center of the sphere
        Vector3 directionToSphereCenter = (sphereObject.transform.position - transform.position).normalized;

        // Calculate the distance from the object's current position to the surface of the sphere
        float distanceToSphereSurface = Vector3.Distance(transform.position, sphereObject.transform.position);

        // If the object is outside the sphere, move it towards the sphere's surface
        if (distanceToSphereSurface > sphereRadius)
        {
            // Move the object towards the surface of the sphere
            transform.position += directionToSphereCenter * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Set the flag to indicate that the object has reached the sphere's surface
            reachedSphere = true;
        }
    }

    void MoveRandomlyInsideSphere()
    {
        // Apply random forces to the object to simulate random motion inside the sphere
        Vector3 randomForce = Random.insideUnitSphere.normalized * Random.Range(minForce, maxForce);
        GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);

        // Clamp the position of the object to keep it inside the sphere
        transform.position = sphereObject.transform.position + Vector3.ClampMagnitude(transform.position - sphereObject.transform.position, sphereRadius);
    }
}
