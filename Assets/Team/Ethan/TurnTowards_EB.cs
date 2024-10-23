using JamesKilpatrick;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TurnTowards_EB : MonoBehaviour
{
    public float turnSpeed = 2;
    public Transform targetObject;
    public Vector3 targetPosition;
    public float singedAngle;
    public Rigidbody rb;

    public int cornerIndex = 0;

    public NavMeshPath_EB nav;

    public float minimumDistanceToCorner = 2f;




    public void Start()
    {
        nav = GetComponent<NavMeshPath_EB>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetDir;



        if (targetObject)
        {
            // Has a target gameobject
            targetDir = (targetObject.position - transform.position).normalized;
        }
        else
        {
            // Just a raw position in the world (for pathfinding points)
            targetDir = (targetPosition - transform.position).normalized;
        }

        singedAngle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);

        rb.AddRelativeTorque(0, singedAngle* turnSpeed, 0);

        // See if we are close to the corner

        if (nav.path != null)
        {
            if (cornerIndex < nav.path.corners.Length)
            {
                if (Vector3.Distance(transform.position, nav.path.corners[cornerIndex]) < minimumDistanceToCorner)
                {
                    cornerIndex++;
                    GetComponent<TurnTowards_EB>().targetPosition = nav.path.corners[cornerIndex];
                }
                else
                {
                    enabled = false;
                }
            }

            
        }

        


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (nav != null)
            if (nav.path != null)
                if (nav.path.corners.Length > 0)
                    Gizmos.DrawSphere(nav.path.corners[cornerIndex], minimumDistanceToCorner / 2f);
    }


}
