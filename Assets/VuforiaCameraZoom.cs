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
    public GameObject magniText,Script1,Demo_reaction_container,Yeast_Reaction,H20Notification,Attractor,H20List,DemoReactionButton, DemoReactionButtonDuplicate;
    

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
    }

    public void Demo_reaction_active()
    {
        Demo_reaction_container.SetActive (true);
        DemoScriptOn();
    }

    public void DemoScriptOn()
    {
        // Access the Demo_attractor component on the targetObject
        Demo_attractor exampleScript = Script1.GetComponent<Demo_attractor>();

        if (exampleScript != null)
        {
            // Set the isActive boolean to true or false
            exampleScript.enabled = true; // or false
        }
        else
        {
            Debug.LogError("Demo_attractor component not found on the target object.");
        }
    }
    public void StartButtonReaction()
    {
        
        AttractAndAttach newscript = Attractor.GetComponent<AttractAndAttach>();
        newscript.enabled = true;
        H20List.SetActive(true);
        
    }

}
