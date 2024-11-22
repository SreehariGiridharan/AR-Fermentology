using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startwith : MonoBehaviour
{
    public GameObject text1,text2,text3;
    // Start is called before the first frame update
    void Start()
    {
       text1.SetActive(true);
       text2.SetActive(false);
       text3.SetActive(false); 
    }
    void OnEnable()
    {
        text1.SetActive(true);
       text2.SetActive(false);
       text3.SetActive(false);
    }

}
