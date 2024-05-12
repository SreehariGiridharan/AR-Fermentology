using UnityEngine;

public class MoveWithinBoundary : MonoBehaviour
{
    public GameObject cylinderObject; // Reference to the cylinder GameObject
    public float moveSpeed = 1.0f; // Speed of movement

    private Rigidbody rb; // Rigidbody component of the object
    private float cylinderRadius, cylinderHeight; // Radius and height of the cylinder
    private bool reachedBoundary = false; // Flag to indicate if the object has reached the boundary
    private Vector3 initialPosition; // Initial position when the boundary is reached

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get the scale of the cylinder
        Vector3 cylinderScale = cylinderObject.transform.localScale;

        // Calculate the radius and height of the cylinder
        cylinderRadius = cylinderScale.x * 0.5f;
        cylinderHeight = cylinderScale.y;
    }

    private void Update()
    {
        Debug.Log("Current position    " + Mathf.Abs(transform.localPosition.x) +"    "+ Mathf.Abs(transform.localPosition.z) +"    "+ transform.localPosition.y );
        Debug.Log("Limiting position   " + cylinderRadius +  "    " +cylinderRadius*0.301491+ "    "+ cylinderHeight );
        Debug.Log("Reached bountary   :"+ reachedBoundary );

       
            // Move the object in the positive x-direction
            rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);

            // Check if the object has reached or exceeded the boundary

            if (Mathf.Abs(transform.localPosition.x) >= cylinderRadius || transform.localPosition.y >= 2 * cylinderHeight || transform.localPosition.y <= 0 || Mathf.Abs(transform.localPosition.z) >= cylinderRadius*2.2852 ||  Mathf.Abs(transform.localPosition.z) <= cylinderRadius*0.301491 )
            {
                // Store the initial position
                initialPosition = transform.localPosition;

                // Reverse the direction of movement
                // moveSpeed *= -1.0f;

                // Set the flag to indicate that the object has reached the boundary
                

                // Rotate the object randomly
                RotateRandomly();
            }
    
    }

    private void RotateRandomly()
    {
        // Generate a random angle
        float randomAngle = Random.Range(0, 180.0f);

        // Apply the rotation
        transform.Rotate(0, randomAngle, 0);
    }
}
