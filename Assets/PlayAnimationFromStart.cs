using UnityEngine;

public class PlayAnimationFromStart : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string animationName; // Name of the animation to play

    // Function to play the animation from the beginning
    public void PlayAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            // Reset the animation state to the start
            animator.Play(animationName, 0, 0f);
        }
    }
}
