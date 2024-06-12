using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Collections;

public class VuforiaCameraZoom : MonoBehaviour
{
    public Camera vuforiaCamera;
    public RawImage rawImage;
    public Vector2 initialSize = new Vector2(540, 1140); // Set your initial RawImage size (width, height)
    public Vector2 targetSize = new Vector2(1920, 1080); // Set your target RawImage size (width, height)
    public float zoomDuration = 2.0f; // Duration for the zoom effect in seconds
    public Vector2 zoomLevel = new Vector2(2.0f, 2.0f); // Set your desired zoom level here (width, height)
    public Vector2 offset = Vector2.zero; // Set your desired offset here (x, y)
    

    private RenderTexture renderTexture;

    void Start()
    {
        // Ensure Vuforia is initialized
        VuforiaApplication.Instance.OnVuforiaStarted += InitializeZoom;

        // Set the RawImage color to transparent
        SetRawImageTransparent();

    }

    private void InitializeZoom()
    {
        // Create a new RenderTexture with the same dimensions as the screen
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        // Assign the RenderTexture to the Vuforia camera
        vuforiaCamera.targetTexture = renderTexture;

        // Assign the RenderTexture to the RawImage component
        rawImage.texture = renderTexture;

        // Set the initial size and scale of the RawImage
        rawImage.rectTransform.sizeDelta = initialSize;
        rawImage.rectTransform.localScale = new Vector3(zoomLevel.x, zoomLevel.y, 1);

        // Adjust the position of the RawImage for the desired offset
        rawImage.rectTransform.anchoredPosition = offset;

        // Start the zoom effect
        StartCoroutine(ZoomEffect());
    }

    void OnDestroy()
    {
        // Clean up the RenderTexture when the script is destroyed
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }

    private void SetRawImageTransparent()
    {
        // Set the RawImage color to transparent
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 255f);
    }

    // Optionally, add methods to adjust zoom and offset dynamically
    public void SetZoomLevel(Vector2 newZoomLevel)
    {
        zoomLevel = newZoomLevel;
        if (rawImage != null)
        {
            rawImage.rectTransform.localScale = new Vector3(zoomLevel.x, zoomLevel.y, 1);
        }
    }

    public void SetOffset(Vector2 newOffset)
    {
        offset = newOffset;
        if (rawImage != null)
        {
            rawImage.rectTransform.anchoredPosition = offset;
        }
    }

    // Optionally, add a method to reset the transparency
    public void SetRawImageAlpha(float alpha)
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);
    }

    // Method to set the size of the RawImage
    public void SetRawImageSize(Vector2 newSize)
    {
        if (rawImage != null)
        {
            rawImage.rectTransform.sizeDelta = newSize;
        }
    }

    // Coroutine to handle the zoom effect
    private IEnumerator ZoomEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / zoomDuration);
            Vector2 currentSize = Vector2.Lerp(initialSize, targetSize, progress);
            SetRawImageSize(currentSize);
            yield return null;
        }

        // Ensure the final size is exactly the target size
        SetRawImageSize(targetSize);
    }
}
