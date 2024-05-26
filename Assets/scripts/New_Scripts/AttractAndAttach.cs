using UnityEngine;
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
    public GameObject spawnPrefab; // Prefab to spawn
    public float attractionForce = 10f; // Strength of attraction force
    public float attachDistance = 0.5f; // Distance threshold for attaching the objects

    private enum PairState { Aligning, Attracting, StopMovement, Attaching, Spawning, Rest }; // States for each pair
    private PairState[] pairStates; // Array to track the state of each pair

    private int currentPairIndex = 0; // Index of the current pair being processed

    private void Start()
    {
        pairStates = new PairState[objectPairs.Count];
        // Start with the first pair
        StartPairState(0, PairState.Aligning);
    }

    private void FixedUpdate()
    {
        switch (pairStates[currentPairIndex])
        {
            case PairState.Aligning:
                RotateObject1TowardsObject2(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2);
                if (IsObject1AlignedWithObject2(objectPairs[currentPairIndex].object1, objectPairs[currentPairIndex].object2))
                {
                    StartPairState(currentPairIndex, PairState.Attracting);
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
                StartPairState(currentPairIndex, PairState.Spawning);
                break;
            case PairState.Spawning:
                SpawnNewObject(objectPairs[currentPairIndex].object1);
                // Move to the next pair
                currentPairIndex++;
                if (currentPairIndex < objectPairs.Count)
                {
                    StartPairState(currentPairIndex, PairState.Aligning);
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

    private void StartPairState(int pairIndex, PairState state)
    {
        if (pairIndex >= 0 && pairIndex < pairStates.Length)
        {
            pairStates[pairIndex] = state;
        }
    }

    private void RotateObject1TowardsObject2(Transform object1, Transform object2)
    {
        Vector3 direction = object2.position - object1.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        object1.rotation = Quaternion.RotateTowards(object1.rotation, targetRotation, Time.fixedDeltaTime * 100f);
    }

    private bool IsObject1AlignedWithObject2(Transform object1, Transform object2)
    {
        Vector3 direction = object2.position - object1.position;
        float angle = Vector3.Angle(object1.forward, direction);
        return Mathf.Approximately(angle, 0f);
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
        object2.parent = object1;
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
           GameObject newObject = Instantiate(spawnPrefab, transform.parent);
            newObject.transform.localPosition = localPosition;
            newObject.transform.localRotation = localRotation;
        }
        else
        {
            // Spawn the object at the final position and rotation
            GameObject newObject = Instantiate(spawnPrefab, spawnPosition.position, spawnPosition.rotation);
        }
        // GameObject newObject = Instantiate(spawnPrefab, spawnPosition.position, spawnPosition.rotation);
        // newObject.transform.localScale = spawnPosition.localScale; // Copy the scale
    }
}
