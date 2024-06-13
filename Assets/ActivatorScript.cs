using UnityEngine;

public class ActivatorScript : MonoBehaviour
{
    public GameObject targetObject; // Reference to the GameObject that has the TargetScript

    public void Started_gameobject()
    {
       targetObject.SetActive (true);
    }
}
