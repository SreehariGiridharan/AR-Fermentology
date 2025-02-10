using UnityEngine;
using System.Collections; // Import IEnumerator interface

public class Notification : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string firstAnimationName; // Name of the first animation to play
    public string secondAnimationName; // Name of the second animation to play
    public float initialDelay = 0f; // Time in seconds before starting the first animation
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
        // Wait for the initial delay before starting the first animation
        yield return new WaitForSeconds(initialDelay);

        // Play the first animation
        animator.Play(firstAnimationName);

        // Wait for the specified delay
        yield return new WaitForSeconds(delayBetweenAnimations);

        // Play the second animation
        animator.Play(secondAnimationName);
    }
}
