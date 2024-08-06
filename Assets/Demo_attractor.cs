using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Demo_attractor : MonoBehaviour
{
    public GameObject targetYeast, targetReactedYeast, targetCo2, targetTriangle, targetSugar,NormalYeastMaterial;
    public Vector3 YeastInitialPosition,ReactedYeastInitialPosition, Co2InitialPosition,TriangleInitialPosition,SugarInitialPosition;

    public Vector3 ReactedYeastRotation, TriangleRotation, Co2Rotation;
   
    public UnityEvent onRestart;
    public Transform object1;
    public Transform object2;

    public GameObject spawnPrefab1, spawnPrefab2, textObject,moving_distance,YeastReaction,DemoReaction,  ReactedYeastText; // Prefab to spawn
    public float attractionForce = 10f; // Strength of attraction force
    public float attachDistance = 0.5f; // Distance threshold for attaching the objects

    private enum PairState { Waiting, Scriptoff, Aligning, Attracting, StopMovement, Attaching, Spawning, Rest }; // States for the pair
    private PairState pairState = PairState.Waiting; // Initial state

    public Material material1;
    public Material material2;
    public Material material3;
    public float blinkInterval = 0.5f; // Time in seconds between blinks
    public float initialWaitTime = 2.0f; // Time to wait before starting the Scriptoff state

    private bool isMaterial1Active = true;
    private bool blinkingStart=true;
    private Coroutine blinkCoroutine;

    private bool activationbool = false;
    



    private void Start()
    {
        StartCoroutine(StartAfterDelay(initialWaitTime));
        
    }

    private void FixedUpdate()
    {
        switch (pairState)
        {
            case PairState.Waiting:
                // Do nothing, waiting for the delay to end
                targetTriangle.SetActive(false);
                targetCo2.SetActive(false);
                break;

            case PairState.Scriptoff:
                textObject.SetActive(false);
                ScriptDeactivater(object1, object2, activationbool);
                StartPairState(PairState.Aligning);
                string childName0 = "Yeast";
                Transform child0 = object1.Find(childName0);

                Renderer renderer1 = object2.GetComponent<Renderer>();
                if (renderer1 != null)
                {
                    if (blinkCoroutine != null)
                    {
                        StopCoroutine(blinkCoroutine);
                    }
                    blinkCoroutine = StartCoroutine(BlinkMaterials(child0, object2));
                }
                break;

            case PairState.Aligning:
                RotateObject1TowardsObject2(object1, object2);
                if (IsObject1AlignedWithObject2(object1, object2))
                {
                    StartPairState(PairState.Attracting);
                }
                break;

            case PairState.Attracting:
                AttractObjects(object1, object2);
                if (AreObjectsCloseEnough(object1, object2))
                {
                    StartPairState(PairState.StopMovement);
                }
                break;

            case PairState.StopMovement:
                StopMovement(object1, object2);
                StartPairState(PairState.Attaching);
                break;

            case PairState.Attaching:
                AttachObjects(object1, object2);
                StartCoroutine(StartDelay(2f));
                break;

            case PairState.Spawning:
                // SpawnNewObject(object1);
                // Move to the Rest state
                MoveStraight script6 = moving_distance.GetComponent<MoveStraight>();
                script6.enabled = true;
                // moving_distance.SetActive(true);
                StartPairState(PairState.Rest);
                
                break;

            case PairState.Rest:
                // No action needed, just stay idle
                // YeastReaction.SetActive(true);
                // DemoReaction.SetActive(false);
                // Debug.Log("finished");
                break;
        }
    }

    private IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartPairState(PairState.Scriptoff);
    }

    private IEnumerator BlinkMaterials(Transform obj, Transform obj2)
    {
        Renderer objRenderer1 = obj.GetComponent<Renderer>();
        Renderer objRenderer2 = obj2.GetComponent<Renderer>();
        blinkingStart=true;
        while (blinkingStart)
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

    private void 
    DeactivateChildByName(Transform parent, string childName, string childName2)
    {
        Transform child = parent.Find(childName);
        Transform child2 = parent.Find(childName2);
        if (child != null && child2 != null)
        {
            child.gameObject.SetActive(true);
            ReactedYeastText.SetActive(true);
            child2.gameObject.SetActive(false);
        }
        else
        {
            // Debug.LogWarning("Child with name " + childName + " not found in " + parent.name);
        }
    }

    private void StartPairState(PairState state)
    {
        pairState = state;
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
        GameObject object2GameObject = object2.gameObject;
        object2GameObject.SetActive(false);
    }

    private void SpawnNewObject(Transform spawnPosition)
    {
        Vector3 position = object1.transform.position;
        Quaternion rotation = object1.transform.rotation;
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

    private void ScriptDeactivater(Transform object1, Transform object2, bool activationbool)
    {
        MoveWithinCircle script1 = object1.GetComponent<MoveWithinCircle>();
        MoveWithinCircle script2 = object2.GetComponent<MoveWithinCircle>();

        if (script1 != null)
        {
            script1.enabled = activationbool;
        }

        if (script2 != null)
        {
            script2.enabled = activationbool;
        }
    }

     private IEnumerator StartDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        string childName = "Reacted_yeast"; // Replace with the actual name of the child you want to deactivate
                string childName2 = "Yeast";
                
                DeactivateChildByName(object1, childName, childName2);
                

                StartPairState(PairState.Spawning);
    }

    public void Restarting()
    {
        
        YeastReaction.SetActive(false);
        DemoReaction.SetActive(true);

        
        onRestart.Invoke();

        Renderer objRenderer1 = NormalYeastMaterial.GetComponent<Renderer>();
        Renderer objRenderer2 = targetSugar.GetComponent<Renderer>();
        objRenderer1.material = material2;
        objRenderer2.material = material1;
        
        
        targetYeast.transform.localPosition = YeastInitialPosition;

        targetReactedYeast.transform.localPosition = ReactedYeastInitialPosition;
        targetReactedYeast.transform.localEulerAngles = ReactedYeastRotation;

        targetCo2.transform.localPosition = Co2InitialPosition;
        targetCo2.transform.localEulerAngles = Co2Rotation;

        targetTriangle.transform.localPosition = TriangleInitialPosition;
        targetTriangle.transform.localEulerAngles = TriangleRotation;

        targetSugar.transform.localPosition= SugarInitialPosition;
        MoveStraight script6 = moving_distance.GetComponent<MoveStraight>();
        
        script6.enabled = false;
        StartCoroutine(StartAfterDelay(initialWaitTime));
        StartPairState(PairState.Waiting);


    }
}
