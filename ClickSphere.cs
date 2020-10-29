using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSphere : MonoBehaviour
{
    public GameObject canvas;
    public GameObject manager;
    public GameObject fill;
    public int location;
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
        if(canvas.activeSelf == true)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
            manager.GetComponent<Main>().currentPlace = location;
            if(fill != null)
            {
                FillBar fb = fill.GetComponent<FillBar>();
                if (fb != null)
                {
                    fb.fillBar();
                }
            }
        }
        
    }
}
