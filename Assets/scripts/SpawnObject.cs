using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectToSpawn; // Reference to the object prefab you want to spawn

    // Function to spawn the object at a specified position
    public void SpawnObjectAtPosition()
    {
        // Get the position and rotation of the animated object after the animation
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // If the spawned object is a child of another object, calculate the local position relative to the parent
        if (transform.parent != null)
        {
            // Calculate the local position of the spawned object relative to the parent
            Vector3 localPosition = transform.parent.InverseTransformPoint(position);
            Quaternion localRotation = Quaternion.Inverse(transform.parent.rotation) * rotation;

            // Spawn the object at the final position and rotation relative to the parent
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.parent);
            spawnedObject.transform.localPosition = localPosition;
            spawnedObject.transform.localRotation = localRotation;
        }
        else
        {
            // Spawn the object at the final position and rotation
            GameObject spawnedObject = Instantiate(objectToSpawn, position, rotation);
        }

        print("Spawn complete");
    }
}
