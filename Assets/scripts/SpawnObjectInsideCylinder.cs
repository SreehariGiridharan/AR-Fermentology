using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectInsideCylinder : MonoBehaviour
{
    public GameObject objectToSpawn; // Reference to the object prefab you want to spawn
    public GameObject cylinderObject; // Reference to the cylinder GameObject
    public GameObject textObject; // Reference to the text GameObject to toggle
    public int maxSpawnedObjects = 10; // Maximum number of spawned objects

    private Queue<GameObject> spawnedObjects = new Queue<GameObject>(); // Queue to store spawned objects
    private int spawnCount = 0; // Counter to track the number of spawns

    // Function to spawn the object inside the cylinder
    public void SpawnObjectInsideCylinderShape()
    {
        // Get the world position, scale, and rotation of the cylinder
        Vector3 cylinderWorldPosition = cylinderObject.transform.position;
        Quaternion cylinderWorldRotation = cylinderObject.transform.rotation;

        // Get the local scale of the cylinder
        Vector3 cylinderLocalScale = cylinderObject.transform.localScale;

        // Generate random positions within the cylinder until a valid position inside the cylinder is found
        Vector3 randomPositionInsideCylinder = Vector3.zero;
        bool isValidPosition = false;
        while (!isValidPosition)
        {
            randomPositionInsideCylinder = new Vector3(
                Random.Range(-cylinderLocalScale.x * 0.5f, cylinderLocalScale.x * 0.5f), // Random x-coordinate within the cylinder's local scale
                Random.Range(0f, cylinderLocalScale.y), // Random y-coordinate within the cylinder's local scale
                Random.Range(-cylinderLocalScale.z * 0.5f, cylinderLocalScale.z * 0.5f) // Random z-coordinate within the cylinder's local scale
            ) + cylinderWorldPosition; // Offset by the cylinder's world position

            // Check if the random position is inside the cylinder
            isValidPosition = IsPositionInsideCylinder(randomPositionInsideCylinder, cylinderWorldPosition, cylinderLocalScale);
        }

        // Generate random rotation
        Quaternion randomRotation = Random.rotation;

        // Spawn the object at the random position with random rotation
        GameObject newObject = Instantiate(objectToSpawn, randomPositionInsideCylinder, randomRotation);

        spawnedObjects.Enqueue(newObject); // Add the spawned object to the queue
        spawnCount++; // Increment the spawn count

        // Toggle the text object's active state when the fifth object is spawned
        if (spawnCount % 5 == 0)
        {
            textObject.SetActive(true); // Activate the text object
            Invoke("DeactivateText", 1f); // Deactivate the text object after 1 second
        }

        // Remove the oldest spawned object if the queue size exceeds the maximum
        if (spawnedObjects.Count > maxSpawnedObjects)
        {
            GameObject oldestObject = spawnedObjects.Dequeue();
            Destroy(oldestObject);
        }

        print("Spawn complete");
    }

    // Function to deactivate the text object
    private void DeactivateText()
    {
        textObject.SetActive(false); // Deactivate the text object
    }

    // Function to check if a position is inside the cylinder
    private bool IsPositionInsideCylinder(Vector3 position, Vector3 cylinderWorldPosition, Vector3 cylinderLocalScale)
    {
        // Calculate the position relative to the center of the cylinder
        Vector3 positionRelativeToCenter = position - cylinderWorldPosition;

        // Check if the position is within the cylinder's local scale
        return Mathf.Abs(positionRelativeToCenter.x) <= cylinderLocalScale.x * 0.5f &&
               positionRelativeToCenter.y >= 0f && positionRelativeToCenter.y <= cylinderLocalScale.y &&
               Mathf.Abs(positionRelativeToCenter.z) <= cylinderLocalScale.z * 0.5f;
    }
}
