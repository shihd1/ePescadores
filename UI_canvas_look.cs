using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UI_canvas_look : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.Log("xxx"+target.forward);
        //transform.LookAt(target);
        this.transform.rotation = Quaternion.LookRotation(target.forward);
    }
}
