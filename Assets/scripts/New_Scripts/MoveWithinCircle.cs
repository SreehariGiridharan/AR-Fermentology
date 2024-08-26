using UnityEngine;

public class MoveWithinCircle : MonoBehaviour
{
    private float moveSpeed = 0.01f; // Speed of movement
    
    public float moveDuration = 5.0f; // Duration of movement in seconds
    private float elapsedTime = 0.0f; // Time elapsed since movement started
    private float angleX, angleY, angleZ;

    bool changer=true;
    public GameObject cubeObject;

    private float  cubeLength,cubeHeight,cubeBreadth;

    private Vector3 cubePos;

    private void Start()
    {

        
    }

    private void Update()
    {
        Vector3 cubeScale = cubeObject.transform.localScale;
        cubePos = cubeObject.transform.localPosition;
        // Calculate the radius and height of the cube
        cubeLength = cubeScale.x;
        cubeHeight = cubeScale.y;
        cubeBreadth = cubeScale.z;
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
                angleX=Random.Range(120.0f, 130.0f);
                angleY=0.0f;
                angleZ=0.0f;
                RotateAngle(angleX, angleY, angleZ);
                changer=false;
                elapsedTime = 0.0f;
                // Debug.Log("angle changed");
            }
        }
        if (transform.localPosition.y > (cubePos.y+(cubeHeight/2)) || transform.localPosition.y < (cubePos.y-(cubeHeight/2)) )
        {
            if (changer)
            {
                angleX=0.0f;
                angleY=Random.Range(120.0f, 130.0f);
                angleZ=0.0f;
                RotateAngle(angleX, angleY, angleZ);
                changer=false;
                elapsedTime = 0.0f;
                // Debug.Log("angle changed");
            }
        }
        if (transform.localPosition.z > (cubePos.z+(cubeBreadth/2)) || transform.localPosition.z < (cubePos.z-(cubeBreadth/2)) )
        {
            if (changer)
            {
                angleX=Random.Range(120.0f, 130.0f);
                angleY=0.0f;
                angleZ=0.0f;
                RotateAngle(angleX, angleY, angleZ);
                changer=false;
                elapsedTime = 0.0f;
                // Debug.Log("angle changed");
            }
        }
        
        transform.position +=transform.forward * moveSpeed * Time.deltaTime;

        
        if (elapsedTime <= moveDuration)
        {
            elapsedTime += Time.deltaTime;
            changer=false;
            // Debug.Log("TIme counter"+ elapsedTime);
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
