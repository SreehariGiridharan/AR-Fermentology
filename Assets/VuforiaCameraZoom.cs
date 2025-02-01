using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Collections;

public class VuforiaCameraZoom : MonoBehaviour
{
    public Camera vuforiaCamera;
    public RawImage rawImage;
    public Canvas canvas; // Reference to the Canvas
    public float zoomDuration = 2.0f; // Duration for the zoom effect in seconds
    public float zoomMultiplier = 2.0f; // Zoom level multiplier
    public Vector2 offset = Vector2.zero; // Set your desired offset here (x, y)
    public GameObject magniText, Script1, Demo_reaction_container, Yeast_Reaction, H20Notification, Attractor, H20List, DemoReactionButton, DemoReactionButtonDuplicate;
    public bool H20ListBool = true;
    public bool AttractionScriptBool = true;

    private RenderTexture renderTexture;
    private Vector2 initialSize;
    private Vector2 targetSize;

    void Start()
    {
        // Ensure Vuforia is initialized
        VuforiaApplication.Instance.OnVuforiaStarted += InitializeZoom;

        // Set the RawImage color to transparent
        SetRawImageTransparent();
    }

    private void InitializeZoom()
    {
        // Calculate the initial and target sizes based on canvas size and zoom multiplier
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        initialSize = new Vector2(canvasRect.rect.width, canvasRect.rect.height);
        targetSize = initialSize * zoomMultiplier;

        // Create a new RenderTexture with the same dimensions as the screen
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        // Assign the RenderTexture to the Vuforia camera
        vuforiaCamera.targetTexture = renderTexture;

        // Assign the RenderTexture to the RawImage component
        rawImage.texture = renderTexture;

        // Set the initial size and scale of the RawImage
        rawImage.rectTransform.sizeDelta = initialSize;
        rawImage.rectTransform.localScale = Vector3.one;

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
    public void SetZoomLevel(float newZoomMultiplier)
    {
        zoomMultiplier = newZoomMultiplier;
        targetSize = initialSize * zoomMultiplier;
    }

    public void SetOffset(Vector2 newOffset)
    {
        offset = newOffset;
        if (rawImage != null)
        {
            rawImage.rectTransform.anchoredPosition = offset;
        }
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
            magniText.SetActive(true);
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / zoomDuration);
            Vector2 currentSize = Vector2.Lerp(initialSize, targetSize, progress);
            SetRawImageSize(currentSize);
            yield return null;
        }
        if (elapsedTime > zoomDuration)
        {
            // reaction.SetActive(true);
            Yeast_Reaction.SetActive(true);
            // Demo_reaction_active(); 
            magniText.SetActive(false);
            StartButtonReaction();
        }

        // Ensure the final size is exactly the target size
        SetRawImageSize(targetSize);

        // Ensure the final size is exactly the target size
        // SetRawImageSize(targetSize);

        // Trigger additional actions after zoom completes
        // magniText.SetActive(false);
        // StartButtonReaction();
    }

    public void Demo_reaction_active()
    {
        Demo_reaction_container.SetActive(true);
        DemoScriptOn();
    }

    public void DemoScriptOn()
    {
        // Access the Demo_attractor component on the targetObject
        Demo_attractor exampleScript = Script1.GetComponent<Demo_attractor>();

        if (exampleScript != null)
        {
            exampleScript.enabled = true; // Activate the script
        }
        else
        {
            Debug.LogError("Demo_attractor component not found on the target object.");
        }
    }

    public void StartButtonReaction()
    {
        AttractAndAttach newscript = Attractor.GetComponent<AttractAndAttach>();

        if (AttractionScriptBool)
        {
            newscript.enabled = true;
        }

        if (H20ListBool)
        {
            H20List.SetActive(true);
        }
    }
}
