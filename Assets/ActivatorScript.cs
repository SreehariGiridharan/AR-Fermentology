using UnityEngine;

public class ActivatorScript : MonoBehaviour
{
    public GameObject targetObject,Demo_reaction_container; // Reference to the GameObject that has the TargetScript

    public void Started_gameobject()
    {
       targetObject.SetActive (true);
       Demo_reaction_container.SetActive (true);

    }
}
