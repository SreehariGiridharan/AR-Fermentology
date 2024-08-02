using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MoveStraight : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject Yeast_Reaction, Demo_reaction, Real_reaction, Reaction_notification, Demo_reaction_button;

    public GameObject TriangleText,Co2Text;

    public float initialWaitTime = 2f;
    public float waitTime = 3f;
    public float waitTimeForDemo = 10f;

    public UnityEvent Object1, Object2;

    public Vector3 direction1 = Vector3.forward;
    public Vector3 direction2 = Vector3.right;

    public float distance1 = 10f;
    public float distance2 = 10f;

    public float speed1 = 1f;
    public float speed2 = 1f;

    private Vector3 startPosition1;
    private Vector3 startPosition2;

    private bool isMoving1 = true;
    private bool isMoving2 = true;

    private bool hasInvoked1 = false;
    private bool hasInvoked2 = false;

    private bool hasExecuted = false;

    void Start()
    {
        InitializePositions();
        StartCoroutine(StartAfterDelay(initialWaitTime));
    }

    void OnEnable()
    {
        hasExecuted = false;
        isMoving1 = true;
        isMoving2 = true;
        hasInvoked1 = false;
        hasInvoked2 = false;
        InitializePositions();
        ExecuteFunctionOnce();
    }

    void InitializePositions()
    {
        if (object1 != null) startPosition1 = object1.transform.position;
        if (object2 != null) startPosition2 = object2.transform.position;
    }

    void ExecuteFunctionOnce()
    {
        if (!hasExecuted)
        {
            Debug.Log("Function executed!");
            StartCoroutine(StartAfterDelay(initialWaitTime));
            hasExecuted = true;
        }
    }

    private IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        object1.SetActive(true);
        object2.SetActive(true);
        TriangleText.SetActive(true);
        Co2Text.SetActive(true);
    }

    void Update()
    {
        if (startPosition1 == Vector3.zero || startPosition2 == Vector3.zero) return;

        if (isMoving1 && Vector3.Distance(startPosition1, object1.transform.position) < distance1)
        {
            object1.transform.Translate(direction1.normalized * speed1 * Time.deltaTime);
        }
        else if (isMoving1 && !hasInvoked1)
        {
            isMoving1 = false;
            hasInvoked1 = true;
            StartCoroutine(WaitIdle(waitTime, () =>
            {
                Object1.Invoke();
                StartCoroutine(AnotherCoroutine(waitTimeForDemo));
            }));
        }

        if (isMoving2 && Vector3.Distance(startPosition2, object2.transform.position) < distance2)
        {
            object2.transform.Translate(direction2.normalized * speed2 * Time.deltaTime);
        }
        else if (isMoving2 && !hasInvoked2)
        {
            isMoving2 = false;
            hasInvoked2 = true;
            StartCoroutine(WaitIdle(waitTime, () =>
            {
                Object2.Invoke();
                StartCoroutine(AnotherCoroutine(waitTimeForDemo));
            }));
        }
    }

    private IEnumerator WaitIdle(float waitTime, UnityAction onComplete)
    {
        yield return new WaitForSeconds(waitTime);
        onComplete?.Invoke();
    }

    private IEnumerator AnotherCoroutine(float waitTime)
    {
        Reaction_notification.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Yeast_Reaction.SetActive(true);
        Demo_reaction.SetActive(false);
        Real_reaction.SetActive(true);
        Demo_reaction_button.SetActive(true);
    }
}
