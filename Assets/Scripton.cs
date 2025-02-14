using System.Collections;
using UnityEngine;

public class Scripton : MonoBehaviour
{
    public GameObject YeastUpdated;
    public float delayTime = 2.0f; // Time delay in seconds
    public enum ObjectState
    {
        Temp5,
        Temp35,
        Temp100
    }

    public static ObjectState currentTempState;

    void Start()
    {
        switch (currentTempState)
        {
            case ObjectState.Temp5:
                delayTime = 15.0f;
                
                break;
            case ObjectState.Temp35:
                delayTime = 2.0f;
                break;
            case ObjectState.Temp100:
                delayTime = 2.0f;
                break;
        }
        // Start the coroutine to enable the script after a delay
        StartCoroutine(EnableScriptWithDelay());
    }

    private IEnumerator EnableScriptWithDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Enable the MoveWithinCircle script after the delay
        MoveWithinCircle exampleScript2 = YeastUpdated.GetComponent<MoveWithinCircle>();
        exampleScript2.enabled = true;
    }
}

