using UnityEngine;
using System.Collections;

public class TimedAction : MonoBehaviour
{
    public float actionDuration = 5f; // Duration for the action to happen in seconds
    public Animator animator; // Reference to the Animator component
    public string firstAnimationName; // Name of the first animation to play
    public string secondAnimationName; // Name of the second animation to play
    public GameObject name, symbol;

    private bool swapping=false;

    public void Swaptext()
    {
        

       StartCoroutine(PerformActionForDuration(actionDuration)); 
    }

    private IEnumerator PerformActionForDuration(float duration)
    {
        // Set the flag to indicate the action is active
        animator.Play(firstAnimationName);
        
      

        // Wait for the specified delay
        yield return new WaitForSeconds(duration);
          name.SetActive(swapping);
        symbol.SetActive(!swapping);
        swapping=!swapping;

        // Play the second animation
        animator.Play(secondAnimationName);
    }
}
