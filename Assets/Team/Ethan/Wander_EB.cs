using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Wander_EB : MonoBehaviour
{
    
    public Rigidbody rb;

    public float perlin = 100;

    // Update is called once per frame
    void Update()
    {

        Mathf.PerlinNoise1D(Time.time);
        
        //rb.AddRelativeTorque(0, perlin, 0);
        
        
    }
}
