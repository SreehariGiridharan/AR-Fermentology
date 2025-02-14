using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    // public GameObject canvas;
    // Start is called before the first frame update
   public void ResetActivate()
   {
    FindObjectOfType<TaskManager>().ResetTaskProgress();    
    // canvas.SetActive(true);
   }
}
