using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward_EB : MonoBehaviour
{

    public Rigidbody rb;

    public float speed = 100f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(0, 0, speed);
    }
}
