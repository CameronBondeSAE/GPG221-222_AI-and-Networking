using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTargetFar : MonoBehaviour
{
    //variabels for detection radius/layer and player transform 
    public float targetCloseDetection = 5f;
    public LayerMask detectionLayer;
    public Transform playerTransform;
    EvilMarbleSensorss EvilMarbleSensorss;
    public Rigidbody EvilRB;

    private bool playerIsDetected;

    public void Start()
    {
        EvilMarbleSensorss = GetComponent<EvilMarbleSensorss>();
        EvilRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DetectCloseCircle();
    }

    //Uses a circle collider to detect the player using a layer
    public void DetectCloseCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, detectionLayer);
        bool playerDetectedFar = false;

        foreach (var hitCollider in hitColliders)
        {
            //Shoot out a raycast to check is player is behind a wall
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, targetCloseDetection))
            {
                Debug.DrawRay(transform.position, directionToTarget * targetCloseDetection, Color.green, 1f);

                //If player is in radius and not behind a wall, change state
                if (hit.collider == hitCollider)
                {
                    playerTransform = hitCollider.transform;
                    EvilMarbleSensorss.SeeTargetClose = true;
                    EvilMarbleSensorss.IsHome = false;
                    playerDetectedFar = true;
                    playerIsDetected = true;
                    EvilRB.velocity = Vector3.zero;
                    break;
                }
            }
        }

        //If player leaves radius, reset to base state
        if (!playerDetectedFar && playerIsDetected)
        {
            ResetStates();
        }
    }

    public void ResetStates()
    {
        EvilMarbleSensorss.SeeTargetClose = false;
        EvilMarbleSensorss.IsBlocking = false;
        playerIsDetected = false;
        EvilRB.velocity = Vector3.zero;
    }

    //show radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);
    }
}
