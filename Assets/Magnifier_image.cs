using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class Magnifier_image : MonoBehaviour
{
    public Vector2 imageSize = new Vector2(540, 1140); // Desired size for the RawImage
    public Vector2 offset = Vector2.zero;
    public RawImage rawImage;
    private RenderTexture renderTexture;
    private Camera vuforiaCamera;

    void Start()
    {
        // Make RawImage transparent initially
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0f);
    }

    public void Zoom_started()
    {
        // Find the Vuforia ARCamera
        vuforiaCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();

        if (vuforiaCamera == null)
        {
            Debug.LogError("Vuforia ARCamera not found.");
            return;
        }

        // Set up the RenderTexture with the size of the image
        renderTexture = new RenderTexture((int)imageSize.x, (int)imageSize.y, 24);
        vuforiaCamera.targetTexture = renderTexture;

        // Assign the RenderTexture to the RawImage component
        rawImage.texture = renderTexture;

        // Set the size and scale of the RawImage
        rawImage.rectTransform.sizeDelta = imageSize;
        rawImage.rectTransform.localScale = Vector3.one;

        // Adjust the position of the RawImage for the desired offset
        rawImage.rectTransform.anchoredPosition = offset;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 1f); // Set alpha to 1 (fully opaque)
    }

    // This method can be called to adjust the zoom level
    public void Zoomed(Vector2 zoomLevel)
    {
        // Update the scale of the RawImage based on the zoom level
        rawImage.rectTransform.localScale = new Vector3(zoomLevel.x, zoomLevel.y, 1);
    }

    void OnDestroy()
    {
        // Clean up the RenderTexture when the object is destroyed
        if (renderTexture != null)
        {
            renderTexture.Release();
            renderTexture = null;
        }

        if (vuforiaCamera != null)
        {
            vuforiaCamera.targetTexture = null;
        }
    }
}
