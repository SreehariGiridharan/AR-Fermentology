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

    private enum PairState { Aligning, Attracting, StopMovement, Attaching, Spawning }; // States for each pair
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
                    // All pairs processed, reset to the first pair
                    currentPairIndex = 0;
                    StartPairState(currentPairIndex, PairState.Aligning);
                    Debug.Log("finished");
                }
                break;
        }
    }

    private void StartPairState(int pairIndex, PairState state)
    {
        pairStates[pairIndex] = state;
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
        float angle = Vector3.Angle(object1.right, direction);
        return Mathf.Approximately(angle, 90f);
    }

    private void AttractObjects(Transform object1, Transform object2)
    {
        Vector3 direction = object1.position - object2.position;
        object1.GetComponent<Rigidbody>().AddForce(-direction.normalized * attractionForce * Time.fixedDeltaTime);
        object2.GetComponent<Rigidbody>().AddForce(direction.normalized * attractionForce * Time.fixedDeltaTime);
    }

    private bool AreObjectsCloseEnough(Transform object1, Transform object2)
    {
        Vector3 direction = object1.position - object2.position;
        return direction.magnitude <= attachDistance;
    }

    private void StopMovement(Transform object1, Transform object2)
    {
        object1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        object2.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void AttachObjects(Transform object1, Transform object2)
    {
        object2.parent = object1;
        GameObject object2GameObject = object2.gameObject;
        object2GameObject.SetActive(false);
    }

    private void SpawnNewObject(Transform spawnPosition)
    {
        Instantiate(spawnPrefab, spawnPosition.position, Quaternion.identity);
    }
}
