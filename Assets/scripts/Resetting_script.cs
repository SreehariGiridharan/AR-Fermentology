using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetting_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();  
    }

    
}
