using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noiseping : MonoBehaviour
{
    public float debugTimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugTimer -= Time.deltaTime;

        if(debugTimer < 0 )
        {
            Destroy(gameObject);
        }
    }
}
