using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material newMaterial; // Reference to the new material to apply

    // Function to change the material
    public void ChangeMaterial()
    {
        // Check if a renderer component is attached to the GameObject
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Change the material to the new material
            renderer.material = newMaterial;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on GameObject.");
        }
    }
}
