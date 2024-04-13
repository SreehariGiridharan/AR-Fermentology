using UnityEngine;

public class SpawnObjectbasedonobject : MonoBehaviour
{
    public GameObject objectToSpawn; // Reference to the object prefab you want to spawn
    public GameObject spawnPositionObject; // Reference to the GameObject whose position will be used as the spawn position

    // Function to spawn the object at a specified position
    public void SpawnObjectAtPosition()
    {
        Vector3 spawnPosition; // Position to spawn the object

        // If a spawn position object is specified, use its position as the spawn position
        if (spawnPositionObject != null)
        {
            spawnPosition = spawnPositionObject.transform.position;
        }
        else
        {
            spawnPosition = transform.position; // Use the current object's position as the spawn position
        }

        // Generate a random rotation
        Quaternion randomRotation = Random.rotation;

        // If the spawned object is a child of another object, calculate the local position relative to the parent
        if (transform.parent != null)
        {
            // Calculate the local position of the spawned object relative to the parent
            Vector3 localPosition = transform.parent.InverseTransformPoint(spawnPosition);
            Quaternion localRotation = Quaternion.Inverse(transform.parent.rotation) * randomRotation;

            // Spawn the object at the final position and rotation relative to the parent
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.parent);
            spawnedObject.transform.localPosition = localPosition;
            spawnedObject.transform.localRotation = localRotation;
        }
        else
        {
            // Spawn the object at the final position and rotation
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, randomRotation);
        }

        print("Spawn complete");
    }
}
