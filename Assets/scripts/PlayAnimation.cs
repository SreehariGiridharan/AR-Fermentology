using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    public void PlayMyAnimation()
    {
        animator.Rebind(); // Reset the animation state
        animator.Play("complete_task");
    }

}
