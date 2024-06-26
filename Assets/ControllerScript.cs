// ControllerScript.cs
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject targetObject2,magnifyerGlass,DemoReaction; // Assign the GameObject in the Inspector

    
    public void ZoomScriptOn()
    {
        VuforiaCameraZoom exampleScript2 = targetObject2.GetComponent<VuforiaCameraZoom>();

        if (exampleScript2 != null)
        {
            // Set the isActive boolean to true or false
            exampleScript2.enabled = true; // or false
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

    // public void DemoReactionAnimation()
    // {
    //   Notification exampleScript3 = DemoReaction.GetComponent<Notification>();
    //   if (exampleScript3 != null)
    //     {
    //         // Set the isActive boolean to true or false
    //         exampleScript3.enabled = true; // or false
    //     }
    //     else
    //     {
    //         Debug.LogError("VuforiaCameraZoom component not found on the target object.");
    //     }
    // }
}
