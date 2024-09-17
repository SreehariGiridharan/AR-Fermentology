using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarTemp : MonoBehaviour
{
    public Image progressBar; // Reference to the Image component
    public float duration = 5f; // Duration in seconds for the progress bar to fill

    private float elapsedTime = 0f; // Time elapsed since the progress started
    private Coroutine progressCoroutine;

    void OnEnable()
    {
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
    }

    private IEnumerator FillProgressBar()
    {
        while (elapsedTime < duration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the fill amount (0 to 1)
            float fillAmount = elapsedTime / duration;

            // Update the fill amount of the progress bar
            progressBar.fillAmount = fillAmount;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the fill amount is set to 1 at the end
        progressBar.fillAmount = 1f;
    }
}
