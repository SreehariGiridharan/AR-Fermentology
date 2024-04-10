using UnityEngine;

public class ChangeRenderingMode : MonoBehaviour
{
    public Material material; // Reference to the material whose rendering mode you want to change

    void Start()
    {
        // Ensure the material reference is not null
        MaterialChange();
        
    }
    void MaterialChange()
        {
        if(material != null)
        {
            // Change the rendering mode to transparent
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
        else
        {
            Debug.LogError("Material reference is null. Please assign a material to the script.");
        }
        }
}
