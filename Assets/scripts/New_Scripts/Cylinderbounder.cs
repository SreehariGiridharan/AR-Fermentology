using UnityEngine;

public class Cylinderbounder : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Speed of movement
    public float moveDuration = 5.0f; // Duration of movement in seconds
    private float elapsedTime = 0.0f; // Time elapsed since movement started
    private float angleX, angleY, angleZ;

    bool changer=true;
    public GameObject cubeObject;

    private float cylinderRadius,cylinderHeight;

    private Vector3 cubePos;
    private Vector3 LocalPosition;
    public GameObject childObject;
    public Rigidbody rb;
    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
        Vector3 cubeScale = cubeObject.transform.localScale;
        cubePos = cubeObject.transform.localPosition;
        // Calculate the radius and height of the cube
        cylinderRadius = cubeScale.x/2;
        cylinderHeight = cubeScale.y/2;
        // Transform childTransform = transform.Find("childObject");
        
        
    }

    private void Update()
    {
        // Calculate direction towards the circle center
       
        // Move the object in that direction
        // Debug.Log(Vector3.Distance(transform.position, circleCenter.position));
        // Debug.Log( "Length  height breadth   "+ cubeLength+"  " + cubeHeight+"  " + cubeBreadth);
        // Debug.Log( "Position of cube  "+ cubePos.x+"  "+cubePos.y+"  "+cubePos.z);
        // Debug.Log("Movement of cube  "+ transform.LocalPosition.x+"  " + transform.LocalPosition.y+"  " + transform.LocalPosition.z);
        
       Transform childTransform = childObject.transform;
         if (childTransform != null)
        {
            // Get the local position of the child object relative to its parent
            LocalPosition = childTransform.localPosition;

            // Print the local position
            Debug.Log("Local Position of Child Object: " + LocalPosition);
        }
        else
        {
            Debug.LogWarning("Child object not found.");
        } 
       

        Debug.Log("X"+(LocalPosition.x-cubePos.x));
        Debug.Log("Z"+(LocalPosition.z-cubePos.z));
        Debug.Log("Radius check"+ (((LocalPosition.x-cubePos.x)*(LocalPosition.x-cubePos.x))+((LocalPosition.z-cubePos.z)*(LocalPosition.z-cubePos.z))) );
        Debug.Log("Radius limit"+ cylinderRadius*cylinderRadius);

        
        if (((LocalPosition.x*LocalPosition.x)+(LocalPosition.z+cubePos.z*LocalPosition.z+cubePos.z)) < cylinderRadius*cylinderRadius)
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
        // if (transform.localPosition.y > (cubePos.y+(cylinderHeight/2)) || transform.localPosition.y < (cubePos.y-(cylinderHeight/2)) )
        // {
        //     if (changer)
        //     {
        //         angleX=150.0f;
        //         angleY=0.0f;
        //         angleZ=0.0f;
        //         RotateAngle(angleX, angleY, angleZ);
        //         changer=false;
        //         elapsedTime = 0.0f;
        //         Debug.Log("angle changed");
        //     }
        // }
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
