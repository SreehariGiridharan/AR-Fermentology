using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarTemp : MonoBehaviour
{
    public enum ObjectState
    {
        Temp5,
        Temp35,
        Temp100
    }
    public ObjectState currentTempState;
    public Image progressBar; // Reference to the Image component
    public float duration = 5f; // Duration in seconds for the progress bar to fill
    public Button targetButton1, targetButton2, targetButton3; // Reference to the Button components
    public Button pauseResumeButton;  // Button to pause/resume the progress
    public GameObject ProcessIncompleteButton;
    private float elapsedTime = 0f; // Time elapsed since the progress started
    private Coroutine progressCoroutine;
    private bool isPaused = false; // To keep track of whether the process is paused or not

    public Animator animator; // Reference to the Animator component
    public string animationName;
    public int determiner = 0;
     public Image targetImage1, targetImage2, targetImage3; // Reference to the Image component
    void OnEnable()
    {
         switch (currentTempState)
        {
            case ObjectState.Temp5:
                determiner = 3;
                
                    
                break;
            case ObjectState.Temp35:
                determiner = 1;
                
                break;
            case ObjectState.Temp100:
                determiner = 2;
                
                break;
        }
        // Set buttons to non-interactable at the start
        if (targetButton1 != null)
            targetButton1.interactable = false;

        if (targetButton2 != null)
            targetButton2.interactable = false;

        if (targetButton3 != null)
            targetButton3.interactable = false;

        if (ProcessIncompleteButton != null)
            ProcessIncompleteButton.SetActive(true);

        // Ensure the fill amount is set to 0 at the start
        progressBar.fillAmount = 0f;

        // Reset the elapsed time
        elapsedTime = 0f;

        // Start the coroutine to fill the progress bar
        if (progressCoroutine != null)
        {
            StopCoroutine(progressCoroutine);
        }
        progressCoroutine = StartCoroutine(FillProgressBar());

        // Add listener to the pause/resume button
        if (pauseResumeButton != null)
        {
            pauseResumeButton.onClick.AddListener(TogglePauseResume);
        }
    }

    private IEnumerator FillProgressBar()
    {
        while (elapsedTime < duration)
        {
            // If paused, stop the progress
            if (!isPaused)
            {
                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                // Calculate the fill amount (0 to 1)
                float fillAmount = elapsedTime / duration;

                // Update the fill amount of the progress bar
                progressBar.fillAmount = fillAmount;
            }

            // Wait until the next frame
            yield return null;
        }

        // Ensure the fill amount is set to 1 at the end
        progressBar.fillAmount = 1f;

        // Call the event when progress is complete
        OnProgressComplete();
    }

    // Event triggered when progress bar is filled
    private void OnProgressComplete()
    {
        Debug.Log("Progress bar is filled!");

        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            // Reset the animation state to the start
            animator.Play(animationName, 0, 0f);
        }

        // Enable the buttons once the progress bar is fully filled
        if (determiner == 1)
        {
            if (targetButton1 != null)
            {
                targetButton1.interactable = true;  // Enable the button
            }

            if (targetButton2 != null)
            {
                targetButton2.interactable = false;  // Enable the button
            }

            if (targetButton3 != null)
            {
                targetButton3.interactable = false;  // Enable the button
            }

            if (ProcessIncompleteButton != null)
            {
                ProcessIncompleteButton.SetActive(false);  // Disable the button
            }
        }
        else if (determiner == 2)
        {
            if (targetButton1 != null)
            {
                targetButton1.interactable = false;  // Enable the button
            }

            if (targetButton2 != null)
            {
                targetButton2.interactable = true;  // Enable the button
            }

            if (targetButton3 != null)
            {
                targetButton3.interactable = false;  // Enable the button
            }

            if (ProcessIncompleteButton != null)
            {
                ProcessIncompleteButton.SetActive(false);  // Disable the button
            }
        }
        else if (determiner == 3)
        {
            if (targetButton1 != null)
            {
                targetButton1.interactable = false;  // Enable the button
            }

            if (targetButton2 != null)
            {
                targetButton2.interactable = false;  // Enable the button
            }

            if (targetButton3 != null)
            {
                targetButton3.interactable = true;  // Enable the button
            }

            if (ProcessIncompleteButton != null)
            {
                ProcessIncompleteButton.SetActive(false);  // Disable the button
            }
        }
        // if (targetButton1 != null)
        // {
        //     targetButton1.interactable = true;  // Enable the button
        // }

        // if (targetButton2 != null)
        // {
        //     targetButton2.interactable = false;  // Enable the button
        // }

        // if (targetButton3 != null)
        // {
        //     targetButton3.interactable = false;  // Enable the button
        // }

        // if (ProcessIncompleteButton != null)
        // {
        //     ProcessIncompleteButton.SetActive(false);  // Disable the button
        // }
    }

    // Method to toggle pause and resume
    public void TogglePauseResume()
    {
        isPaused = !isPaused;  // Toggle the pause/resume state
        Debug.Log(isPaused ? "Progress paused" : "Progress resumed");
    }
}
