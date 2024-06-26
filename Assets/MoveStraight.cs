using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MoveStraight : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject Yeast_Reaction, Demo_reaction, Real_reaction; // Reference to the Yeast_Reaction GameObject

    public float waitTime = 3f;

    public UnityEvent Object1, Object2;

    public Vector3 direction1 = Vector3.forward; // Direction for object1 to move
    public Vector3 direction2 = Vector3.right;   // Direction for object2 to move

    public float distance1 = 10f; // Distance for object1 to move
    public float distance2 = 10f; // Distance for object2 to move

    public float speed1 = 1f;     // Speed for object1
    public float speed2 = 1f;     // Speed for object2

    private Vector3 startPosition1; // Starting position for object1
    private Vector3 startPosition2; // Starting position for object2

    private bool isMoving1 = true; // Flag to check if object1 is moving
    private bool isMoving2 = true; // Flag to check if object2 is moving

    private bool hasInvoked1 = false; // Flag to ensure event is invoked only once for object1
    private bool hasInvoked2 = false; // Flag to ensure event is invoked only once for object2

    void Start()
    {
        // Initialize starting positions
        startPosition1 = object1.transform.position;
        startPosition2 = object2.transform.position;
        object1.SetActive(true);
        object2.SetActive(true);
    }

    void Update()
    {
        // Move object1 if it's set to move
        if (isMoving1 && Vector3.Distance(startPosition1, object1.transform.position) < distance1)
        {
            object1.transform.Translate(direction1.normalized * speed1 * Time.deltaTime);
        }
        else if (isMoving1 && !hasInvoked1)
        {
            isMoving1 = false; // Stop moving when the distance is reached
            hasInvoked1 = true; // Ensure the event is invoked only once
            StartCoroutine(WaitIdle(waitTime, () =>
            {
                Object1.Invoke();
                StartCoroutine(AnotherCoroutine(2f)); // Start another coroutine
            }));
        }

        // Move object2 if it's set to move
        if (isMoving2 && Vector3.Distance(startPosition2, object2.transform.position) < distance2)
        {
            object2.transform.Translate(direction2.normalized * speed2 * Time.deltaTime);
        }
        else if (isMoving2 && !hasInvoked2)
        {
            isMoving2 = false;
            hasInvoked2 = true; // Ensure the event is invoked only once
            StartCoroutine(WaitIdle(waitTime, () =>
            {
                Object2.Invoke(); // Stop moving when the distance is reached
                StartCoroutine(AnotherCoroutine(15f)); // Start another coroutine
            }));
        }
    }

    private IEnumerator WaitIdle(float waitTime, UnityAction onComplete)
    {
        Debug.Log("Waiting starts at: " + Time.time);
        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waiting ends at: " + Time.time);
        // Invoke the onComplete action
        onComplete?.Invoke();
    }

    private IEnumerator AnotherCoroutine(float waitTime)
    {
        Debug.Log("Another coroutine starts at: " + Time.time);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Another coroutine ends at: " + Time.time);
        Yeast_Reaction.SetActive(true); // Deactivate the Yeast_Reaction GameObject
        Demo_reaction.SetActive(false);
        Real_reaction.SetActive(true);
    }
}
