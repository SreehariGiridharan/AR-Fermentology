using UnityEngine;

public class BarcodeScanner : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator component
    public string animationName;  // Name of the animation to play

    // This method is called when the barcode is scanned
    public void OnBarcodeScanned()
    {
        PlayAnimationFromStart();
    }

    // Method to play the animation from the start
    private void PlayAnimationFromStart()
    {
        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            // Start the animation from the beginning
            animator.Play(animationName, 0, 0f);
        }
    }
}
