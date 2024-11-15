using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startcode : MonoBehaviour
{
     public enum ObjectState
    {
        Temp5,
        Temp35,
        Temp100
    }
    public ObjectState currentTempState;
    public Image targetImage1, targetImage2, targetImage3;
    public GameObject object1, object2, object3;
    // Start is called before the first frame update
    void Start()
    {
        switch (currentTempState)
        {
            case ObjectState.Temp5:
                if (targetImage1 != null)
                {
                    // Set the color to red
                    targetImage1.color = Color.red;
                }
                else
                {
                    Debug.LogError("No Image component assigned in the Inspector");
                }
                object1.SetActive(true);    
                break;
            case ObjectState.Temp35:
    
                if (targetImage2 != null)
                {
                    // Set the color to red
                    targetImage2.color = Color.red;
                }
                else
                {
                    Debug.LogError("No Image component assigned in the Inspector");
                }
                object2.SetActive(true);
                break;
            case ObjectState.Temp100:
                
                if (targetImage3 != null)
                {
                    // Set the color to red
                    targetImage3.color = Color.red;
                }
                else
                {
                    Debug.LogError("No Image component assigned in the Inspector");
                }
                object3.SetActive(true);
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
