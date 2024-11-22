using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCode : MonoBehaviour
{
    public enum ObjectState
    {
        Temp5,
        Temp35,
        Temp100
    }

    public ObjectState currentTempState;

    // UI Image references for different temperature states
    public Image[] targetImages; // Drag and drop the images in order (Temp5, Temp35, Temp100)
    public GameObject[] objects; // Drag and drop corresponding GameObjects in the Inspector
    public GameObject[] targetObjects; // Drag and drop all objects you want to update

    // Speeds for different temperature states
    public float Temp5_speed = 0.001f;
    public float Temp35_speed = 0.008f;
    public float Temp100_speed = 0.04f;

    private float speed = 0.0f;

    void Start()
    {
        // Validate inputs
        if (targetImages.Length != 3 || objects.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 images and 3 objects for each state.");
            return;
        }

        if (targetObjects == null || targetObjects.Length == 0)
        {
            Debug.LogError("Please assign target objects in the Inspector.");
            return;
        }

        // Determine state and update corresponding settings
        switch (currentTempState)
        {
            case ObjectState.Temp5:
                UpdateState(0, Temp5_speed);
                break;

            case ObjectState.Temp35:
                UpdateState(1, Temp35_speed);
                break;

            case ObjectState.Temp100:
                UpdateState(2, Temp100_speed);
                break;
        }

        // Update all target objects' move speed
        foreach (GameObject obj in targetObjects)
        {
            MoveWithinCircle moveScript = obj.GetComponent<MoveWithinCircle>();
            if (moveScript != null)
            {
                moveScript.moveSpeed = speed;
            }
            else
            {
                MoveWithinCircle1 moveScript1 = obj.GetComponent<MoveWithinCircle1>();
                if (moveScript1 != null)
                {
                    moveScript1.moveSpeed = speed;
                }
                else
                {
                    Debug.LogError($"No MoveWithinCircle or MoveWithinCircle1 script found on {obj.name}");
                }
            }
        }
    }

    void UpdateState(int index, float newSpeed)
    {
        // Set the color of the corresponding image to red
        if (targetImages[index] != null)
        {
            targetImages[index].color = Color.red;
        }

        // Activate the corresponding object
        if (objects[index] != null)
        {
            objects[index].SetActive(true);
        }

        // Update speed
        speed = newSpeed;
    }

    void Update()
    {
        Debug.Log("Speed is " + speed);
    }
}
