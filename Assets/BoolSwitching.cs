using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolSwitching : MonoBehaviour
{
    private bool state = false;
    private void Start()
    {
        gameObject.SetActive(state);
    }
    // Start is called before the first frame update
    public void BoolToggler()
    {
        state=!state;
        gameObject.SetActive(state);
        
    }
}
