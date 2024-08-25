using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_animation : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
        
        // Play the default animation from the Animator
        if (animator != null)
        {
            animator.Play("Help_button_out"); // Replace "AnimationName" with your animation's name
        }  
    }

  
}
