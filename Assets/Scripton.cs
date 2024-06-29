using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripton : MonoBehaviour
{
    public GameObject YeastUpdated;
    void Start()
    {
       MoveWithinCircle exampleScript2 = YeastUpdated.GetComponent<MoveWithinCircle>(); 
       exampleScript2.enabled = true;
    }

    // Update is called once per frame

}
