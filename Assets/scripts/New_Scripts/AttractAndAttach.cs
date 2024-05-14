

using UnityEngine;

public class AttractAndAttach : MonoBehaviour
{
    public Transform object1; // Reference to the first object
    public Transform object2; // Reference to the second object
    public float attractionForce = 10f; // Strength of attraction force
    public float attachDistance = 0.5f; // Distance threshold for attaching the objects

    public Rigidbody rb1; // Rigidbody component of the first object
    public Rigidbody rb2; // Rigidbody component of the second object

    private bool isAligned = false; // Flag to indicate if object1 is aligned with object2

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isAligned)
        {
            // Rotate object1 towards object2
            RotateObject1TowardsObject2();

            // Check if object1 is aligned with object2
            if (IsObject1AlignedWithObject2())
            {
                isAligned = true;
                Debug.Log("Entered");
            }
        }
        else
        {
            // Calculate direction from object2 to object1
            Vector3 direction = object1.position - object2.position;

            // Apply attraction force to both objects
            rb1.AddForce(-direction.normalized * attractionForce * Time.fixedDeltaTime);
            rb2.AddForce(direction.normalized * attractionForce * Time.fixedDeltaTime);

            // Check if the objects are close enough to attach
            if (direction.magnitude <= attachDistance)
            {
                // Attach the objects together
                AttachObjects();
            }
        }
    }

    private void RotateObject1TowardsObject2()
    {
        Vector3 direction = object2.position - object1.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        object1.rotation = Quaternion.RotateTowards(object1.rotation, targetRotation, Time.fixedDeltaTime * 100f);
    }

    private bool IsObject1AlignedWithObject2()
    {
        Vector3 direction = object2.position - object1.position;
        float angle = Vector3.Angle(object1.right, direction);
        Debug.Log(angle);
        // Debug.Log( Mathf.Approximately(angle, 0f)0);
        return Mathf.Approximately(angle, 90f);
    }

    private void AttachObjects()
    {
        // Make object2 a child of object1
        object2.parent = object1;
    }
}
