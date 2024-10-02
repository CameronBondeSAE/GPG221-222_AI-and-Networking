using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward_EB : MonoBehaviour
{

    public float turnSpeed = 2;
    public Transform targetObject;
    public Vector3 targetPosition;

    public Rigidbody rb;

    public float speed = 100f;

    public float yourTurnDirAndSpeed = 2;

    


    // Start is called before the first frame update
    void Start()
    {
        rb.AddRelativeTorque(0, yourTurnDirAndSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddRelativeForce(0, 0, speed);

        Vector3 targetDir;
        if (targetObject)
        {
            // Has a target gameobject
            targetDir = targetObject.position - transform.position;
        }
        else
        {
            // Just a raw position in the world (for pathfinding points)
            targetDir = targetPosition - transform.position;
        }

        float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
    }

    


    

}
