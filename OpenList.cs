using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenList : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        // this object was clicked - do something
        canvas.SetActive(true);
    }
}
