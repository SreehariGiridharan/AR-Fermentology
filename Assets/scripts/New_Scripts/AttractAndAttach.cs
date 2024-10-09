using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttractAndAttach : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPair
    {
        public Transform object1;
        public Transform object2;
       
    }

    public List<ObjectPair> objectPairs = new List<ObjectPair>(); // List of pairs of objects
    public GameObject spawnPrefab1, spawnPrefab2, Water,H20Notification,DemoReactionButton,DemoReactionButtonDuplicate; // Prefab to spawn
    public float attractionForce = 10f; // Strength of attraction force
    public float attachDistance = 0.5f; // Distance threshold for attaching the objects
    public float StartingDistance = 0.5f; // Distance threshold for starting the reaction
    public float targetAngle2=0.0f;

    private enum PairState { Scriptoff, Aligning, Attracting, StopMovement, Attaching, Spawning, Rest }; // States for each pair
    private PairState[] pairStates; // Array to track the state of each pair
     private PairState currentState;

    private int currentPairIndex = 0; // Index of the current pair being processed
    public Material newMaterial;
    public Material newMaterial1;
    public Material material1;
    public Material material2;
    public Material material3;
    public float blinkInterval = 0.5f; // Time in seconds between blinks

    public float initialWaitTime = 3.0f;
    private Renderer objectRenderer;
    private bool isMaterial1Active = true;
     private Coroutine blinkCoroutine;

     private bool once;
     public Animator animator, animator1;
     public float rotationSpeed = 100f;
     public Vector3 alignmentAxis = Vector3.up;
     public bool H20NotificationBool = true;
     public float DelayAfterH20Disappearance=5.0f;

     public float timeLimit = 5f;  // Set a time limit for Scriptoff state
    private float timeInScriptoff = 0f;  // Track how much time has passed in Scriptoff

    private void Start()
    {
        // if (animator == null)
        // {
        //     animator = GetComponent<Animator>();
        // }
        StartCoroutine(StartAfterDelay(initialWaitTime));
        
    }

     private IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Water.SetActive(false);
        if(H20NotificationBool)
        {
            H20Notification.SetActive(true);
        }
        else
        {
            H20Notification.SetActive(false);
        }
        yield return new WaitForSeconds(DelayAfterH20Disappearance);
        DemoReactionButton.SetActive(true);
        DemoReactionButtonDuplicate.SetActive(false);
        // animator.Play("New Animation_reverse");
        pairStates = new PairState[objectPairs.Count];
        // Start with the first pair
        StartPairState(0, PairState.Scriptoff);
    }

    private void FixedUpdate()
    {
        switch (pairStates[currentPairIndex])
        {
            case PairState.Scriptoff:
            // Increment the time spent in this state
            timeInScriptoff += Time.fixedDeltaTime;

            // Check the distance and time limit
            if (IsDistanceSufficient(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2) || timeInScriptoff >= timeLimit)
            {
                once = true;
                ScriptDeactivater(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2);
                StartPairState(currentPairIndex, PairState.Aligning);
                
                // Reset time counter for the next pair
                timeInScriptoff = 0f;

                // Optional: Update materials or any other specific behavior for Scriptoff state
                string childName0 = "Yeast";
                Transform child0 = objectPairs[currentPairIndex].object1.Find(childName0);
                Renderer renderer1 = objectPairs[currentPairIndex].object2.GetComponent<Renderer>();
                if (renderer1 != null)
                {
                    if (blinkCoroutine != null)
                    {
                        StopCoroutine(blinkCoroutine);
                    }
                    blinkCoroutine = StartCoroutine(BlinkMaterials(child0, objectPairs[currentPairIndex].object2));
                }
            }
            break;

            case PairState.Aligning:

                RotateObject1TowardsObject2(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2, targetAngle2);
                if (IsObject1AlignedWithObject2(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2,targetAngle2))
                {
                    StartPairState(currentPairIndex, PairState.Attracting);
                    Debug.Log("1");
                }
                break;

            case PairState.Attracting:
                AttractObjects(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2);
                if (AreObjectsCloseEnough(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2))
                {
                    StartPairState(currentPairIndex, PairState.StopMovement);
                }
                break;

            case PairState.StopMovement:
                StopMovement(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2);
                StartPairState(currentPairIndex, PairState.Attaching);
                break;

            case PairState.Attaching:
                AttachObjects(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2);
                // string childName = "Reacted_yeast"; // Replace with the actual name of the child you want to deactivate
                // string childName2 = "Yeast";
                // DeactivateChildByName(objectPairs[currentPairIndex].object1, childName, childName2);
                // StartPairState(currentPairIndex, PairState.Spawning);
                StartCoroutine(StartDelay(0f, currentPairIndex));
                break;

            case PairState.Spawning:
                
                if(once)
                {
                SpawnNewObject(objectPairs[currentPairIndex].object1);
                if (animator1 != null)
                {
                    animator1.Play("Height_increasing"); // Replace "AnimationName" with your animation's name
                }
                once=false;
                }
                // Move to the next pair
                currentPairIndex++;
                if (currentPairIndex < objectPairs.Count)
                {
                    StartPairState(currentPairIndex, PairState.Scriptoff);
                }
                else
                {
                    // All pairs processed, move to Rest state
                    Debug.Log("0");
                    currentPairIndex = currentPairIndex - 1;
                    StartPairState(currentPairIndex, PairState.Rest);
                }
                break;

            case PairState.Rest:
                // No action needed, just stay idle
                Debug.Log("finished");
                break;
        }
    }

    private IEnumerator BlinkMaterials(Transform obj, Transform obj2)
    {
        Renderer objRenderer1 = obj.GetComponent<Renderer>();
        Renderer objRenderer2 = obj2.GetComponent<Renderer>();
        while (true)
        {
            // Toggle the active material
            if (isMaterial1Active)
            {
                objRenderer1.material = material2;
                objRenderer2.material = material1;
            }
            else
            {
                objRenderer1.material = material3;
                objRenderer2.material = material3;
            }

            // Flip the boolean flag
            isMaterial1Active = !isMaterial1Active;

            // Wait for the specified interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void DeactivateChildByName(Transform parent, string childName, string childName2)
    {
        Transform child = parent.Find(childName);
        Transform child2 = parent.Find(childName2);
        if (child != null && child2 != null)
        {
            child.gameObject.SetActive(true);
            child2.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Child with name " + childName + " not found in " + parent.name);
        }
    }

    private void StartPairState(int pairIndex, PairState state)
    {
        if (pairIndex >= 0 && pairIndex < pairStates.Length)
        {
            pairStates[pairIndex] = state;
        }
    }

    private void RotateObject1TowardsObject2(Transform object1, Transform object2, float targetAngle2)
    {
        Vector3 direction = object2.position - object1.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        object1.rotation = Quaternion.RotateTowards(object1.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        Quaternion targetRotation2 = Quaternion.Euler(0, targetAngle2, 0);
        object2.rotation = Quaternion.RotateTowards(object2.rotation, targetRotation2, Time.deltaTime * rotationSpeed);
    }

    private bool IsObject1AlignedWithObject2(Transform object1, Transform object2, float targetAngle2)
    {
        Vector3 direction = object2.position - object1.position;
        float angle = Vector3.Angle(object1.forward, direction);

        Quaternion targetRotation2 = Quaternion.Euler(0, targetAngle2, 0);
        float rotationDifference = Quaternion.Angle(object2.rotation, targetRotation2);

        return (Mathf.Approximately(angle, 0f) && Mathf.Approximately(rotationDifference, 0f));
    }
    private void AttractObjects(Transform object1, Transform object2)
    {
        Vector3 direction = object2.position - object1.position;
        float step = attractionForce * Time.fixedDeltaTime;
        object1.position = Vector3.MoveTowards(object1.position, object2.position, step);
        object2.position = Vector3.MoveTowards(object2.position, object1.position, step);
    }

    private bool AreObjectsCloseEnough(Transform object1, Transform object2)
    {
        Vector3 direction = object1.position - object2.position;
        return direction.magnitude <= attachDistance;
    }

    private void StopMovement(Transform object1, Transform object2)
    {
        // No action needed since we aren't using Rigidbody for movement
    }

    private void AttachObjects(Transform object1, Transform object2)
    {
        // object2.parent = object1;
        GameObject object2GameObject = object2.gameObject;
        object2GameObject.SetActive(false);
    }

    private void SpawnNewObject(Transform spawnPosition)
    {
        Vector3 position = objectPairs[currentPairIndex].object1.transform.position;
        Quaternion rotation = objectPairs[currentPairIndex].object1.transform.rotation;
        if (transform.parent != null)
        {
            // Calculate the local position of the spawned object relative to the parent
            Vector3 localPosition = transform.parent.InverseTransformPoint(position);
            Quaternion localRotation = Quaternion.Inverse(transform.parent.rotation) * rotation;

            // Spawn the object at the final position and rotation relative to the parent
            GameObject newObject = Instantiate(spawnPrefab1, transform.parent);
            GameObject newObject1 = Instantiate(spawnPrefab2, transform.parent);

            newObject.transform.localPosition = localPosition;
            newObject.transform.localRotation = localRotation;

            newObject1.transform.localPosition = localPosition;
            newObject1.transform.localRotation = localRotation;
        }
        else
        {
            // Spawn the object at the final position and rotation
            GameObject newObject = Instantiate(spawnPrefab1, spawnPosition.position, spawnPosition.rotation);
            GameObject newObject1 = Instantiate(spawnPrefab2, spawnPosition.position, spawnPosition.rotation);
        }
    }

    private void ScriptDeactivater(Transform object1, Transform object2)
    {
        MoveWithinCircle script1 = object1.GetComponent<MoveWithinCircle>();
        MoveWithinCircle script2 = object2.GetComponent<MoveWithinCircle>();

        if (script1 != null)
        {
            script1.enabled = false;
        }

        if (script2 != null)
        {
            script2.enabled = false;
        }
    }

    private IEnumerator StartDelay(float delay, int currentPairIndex )
    {
        yield return new WaitForSeconds(delay);
        string childName = "Reacted_yeast"; // Replace with the actual name of the child you want to deactivate
        string childName2 = "Yeast";
        DeactivateChildByName(objectPairs[currentPairIndex].object1, childName, childName2);
        StartPairState(currentPairIndex, PairState.Spawning);
    }

    private bool IsDistanceSufficient(Transform object1, Transform object2)
    {
        float distance = Vector3.Distance(object1.position, object2.position);
        Debug.Log("Distance"+ distance+ "StartingDistance"+ StartingDistance);
        return distance >= StartingDistance; // Check if objects are within the threshold distance
    }

}