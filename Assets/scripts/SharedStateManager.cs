using UnityEngine;
using System.Collections.Generic;

public class GlobalStateManager : MonoBehaviour
{
    public enum ObjectState { State1, State2, State3 }

    private static ObjectState sharedState = ObjectState.State1; // Shared state across all scripts

    public List<MonoBehaviour> linkedScripts; // Assign scripts that need to sync

    void Start()
    {
        ApplyState(sharedState);
    }

    public void ChangeState(ObjectState newState)
    {
        sharedState = newState; // Update shared state

        // Apply state to all linked scripts
        foreach (var script in linkedScripts)
        {
            if (script != null)
            {
                script.SendMessage("OnStateChanged", sharedState, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void ApplyState(ObjectState state)
    {
        // This is where you handle state changes (e.g., changing color, behavior, etc.)
        Debug.Log($"{gameObject.name} changed to {state}");
    }

    // Example input to cycle through states (for testing)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeState(ObjectState.State1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeState(ObjectState.State2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeState(ObjectState.State3);
    }
}
