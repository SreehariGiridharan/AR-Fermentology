// ControllerScript.cs
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject targetObject2,magnifyerGlass; 

    
    public void ZoomScriptOn()
    {
        VuforiaCameraZoom exampleScript2 = targetObject2.GetComponent<VuforiaCameraZoom>();

        if (exampleScript2 != null)
        {
            
            exampleScript2.enabled = true; 
        }
        else
        {
            Debug.LogError("VuforiaCameraZoom component not found on the target object.");
        }
    }
    public void Magnifyer()
    {
       magnifyerGlass.SetActive(false); 
    }

}
