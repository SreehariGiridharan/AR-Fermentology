using UnityEngine;

public class MoveWithinCircle : MonoBehaviour
{
    public Transform circleCenter; // Center of the circle
    public float circleRadius = 5.0f; // Radius of the circle
    public float moveSpeed = 1.0f; // Speed of movement
    private Rigidbody rb;
    public float moveDuration = 5.0f; // Duration of movement in seconds
    private float elapsedTime = 0.0f; // Time elapsed since movement started
    private float angleX, angleY, angleZ;

    bool changer=true;
    public GameObject cubeObject;

    private float  cubeLength,cubeHeight,cubeBreadth;

    private Vector3 cubePos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 cubeScale = cubeObject.transform.localScale;
        cubePos = cubeObject.transform.localPosition;
        // Calculate the radius and height of the cube
        cubeLength = cubeScale.x;
        cubeHeight = cubeScale.y;
        cubeBreadth = cubeScale.z;
    }

    private void Update()
    {
        // Calculate direction towards the circle center
       
        // Move the object in that direction
        // Debug.Log(Vector3.Distance(transform.position, circleCenter.position));
        // Debug.Log( "Length  height breadth   "+ cubeLength+"  " + cubeHeight+"  " + cubeBreadth);
        // Debug.Log( "Position of cube  "+ cubePos.x+"  "+cubePos.y+"  "+cubePos.z);
        // Debug.Log("Movement of cube  "+ transform.localPosition.x+"  " + transform.localPosition.y+"  " + transform.localPosition.z);
        
        if (transform.localPosition.x > (cubePos.x+(cubeLength/2)) || transform.localPosition.x < (cubePos.x-(cubeLength/2)) )
        {
            if (changer)
            {
                angleX=0.0f;
                angleY=0.0f;
                angleZ=120.0f;
                RotateAngle(angleX, angleY, angleZ);
                changer=false;
                elapsedTime = 0.0f;
                Debug.Log("angle changed");
            }
        }
        if (transform.localPosition.y > (cubePos.y+(cubeLength/2)) || transform.localPosition.y < (cubePos.y-(cubeLength/2)) )
        {
            if (changer)
            {
                angleX=0.0f;
                angleY=120.0f;
                angleZ=0.0f;
                RotateAngle(angleX, angleY, angleZ);
                changer=false;
                elapsedTime = 0.0f;
                Debug.Log("angle changed");
            }
        }
        if (transform.localPosition.z > (cubePos.z+(cubeLength/2)) || transform.localPosition.z < (cubePos.z-(cubeLength/2)) )
        {
            if (changer)
            {
                angleX=0.0f;
                angleY=0.0f;
                angleZ=120.0f;
                RotateAngle(angleX, angleY, angleZ);
                changer=false;
                elapsedTime = 0.0f;
                Debug.Log("angle changed");
            }
        }
        rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);

        
        if (elapsedTime <= moveDuration)
        {
            elapsedTime += Time.deltaTime;
            changer=false;
            Debug.Log("TIme counter"+ elapsedTime);
        }
        else
        {
            changer=true;
        }
    }

     private void RotateAngle(float angleX,float angleY,float angleZ)
    {
        // Apply the rotation
        transform.Rotate(angleX, angleY, angleZ);
    }
}