using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviours : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleTest(bool active)
    {
        if(active)
        {
            Debug.Log("button is active");
        }
        else
        {
            Debug.Log("button is deactivated");
        }

    }
}
