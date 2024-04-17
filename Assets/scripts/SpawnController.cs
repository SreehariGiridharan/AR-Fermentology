using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public SpawnObjectInsideCylinder spawnScript; // Reference to the script containing the spawn function

    public void Spawnon()
    {
        // Example: Spawn an object when the spacebar is pressed
    
            // Call the function to spawn the object inside the cylinder
            spawnScript.SpawnObjectInsideCylinderShape();

    }
}
