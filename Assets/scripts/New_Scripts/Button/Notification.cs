using UnityEngine;
using System.Collections; // Add this line to import the IEnumerator interface

public class Notification : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string firstAnimationName; // Name of the first animation to play
    public string secondAnimationName; // Name of the second animation to play
    public float delayBetweenAnimations = 2f; // Time in seconds to wait between animations

    private void Start()
    {
        // Start the coroutine to play animations sequentially
        StartCoroutine(PlayAnimations());
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnimations());

    }

    private IEnumerator PlayAnimations()
    {
        // Play the first animation
        animator.Play(firstAnimationName);

        // Wait for the specified delay
        yield return new WaitForSeconds(delayBetweenAnimations);

        // Play the second animation
        animator.Play(secondAnimationName);
    }
}
