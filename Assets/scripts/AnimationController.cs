using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string animationName; // Name of the animation to play

    // Function to play the animation
    public void PlayAnimation()
    {
        // Ensure the animator component is not null
        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            // Play the specified animation from the beginning
            animator.Play(animationName, -1, 0f);
        }
        else
        {
            Debug.LogError("Animator component or animation name is not assigned.");
        }
    }

    // Call this function to play the animation
    public void PlayAnimationInFunction()
    {
        PlayAnimation();
    }
}
