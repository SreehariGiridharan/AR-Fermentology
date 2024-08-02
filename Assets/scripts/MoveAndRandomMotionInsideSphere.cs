using UnityEngine;

public class MoveAndRandomMotionInsideSphere : MonoBehaviour
{
    public GameObject sphereObject; // Reference to the sphere GameObject
    public float moveSpeed = 1f; // The speed of movement towards the sphere
    public float minForce = 0.1f; // Minimum force magnitude for random motion inside the sphere
    public float maxForce = 0.5f; // Maximum force magnitude for random motion inside the sphere

    private float sphereRadius; // The radius of the sphere
    private bool reachedSphere = false; // Flag to indicate if the object has reached the sphere's surface
    private Vector3 randomDirection; // Direction for random motion

    void Start()
    {
        // Get the radius of the sphere
        sphereRadius = sphereObject.GetComponent<SphereCollider>().radius * sphereObject.transform.localScale.x;
    }

    void OnEnable()
    {
        reachedSphere = false;
        Debug.Log("Reached sphere: " + reachedSphere);
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

        Debug.Log("Direction to sphere: " + directionToSphereCenter);

        // Calculate the distance from the object's current position to the surface of the sphere
        float distanceToSphereSurface = Vector3.Distance(transform.position, sphereObject.transform.position);
        Debug.Log("Distance to sphere: " + distanceToSphereSurface);

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
            // Initialize a random direction for motion inside the sphere
            randomDirection = Random.onUnitSphere.normalized;
        }
    }

    void MoveRandomlyInsideSphere()
    {
        // Apply random forces to the object to simulate random motion inside the sphere
        Vector3 randomForce = randomDirection * Random.Range(minForce, maxForce) * Time.deltaTime;

        // Move the object using the random force
        transform.position += randomForce;

        // Clamp the position of the object to keep it inside the sphere
        Vector3 offset = transform.position - sphereObject.transform.position;
        if (offset.magnitude > sphereRadius)
        {
            transform.position = sphereObject.transform.position + offset.normalized * sphereRadius;
        }

        // Change random direction at random intervals for varied motion
        if (Random.value < 0.01f) // Change direction approximately once every 100 frames
        {
            randomDirection = Random.onUnitSphere.normalized;
        }
    }
}
