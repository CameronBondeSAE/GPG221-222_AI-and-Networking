using EB;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SearchPlayerClose : MonoBehaviour
{
    //variables for dection layer/radius and to get the players transform
    public float targetCloseDetection = 5f;
    public LayerMask detectionLayer;
    public Transform playerTransform;
    EvilMarbleSensor EvilMarbleSensor;
    public Rigidbody EvilRb;

    private bool playerIsDetected;


    public void Start()
    {
        EvilMarbleSensor = GetComponent<EvilMarbleSensor>();
        EvilRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        DetectCloseCircle();
    }

    //Uses a circle collider to detect the player by using a layer
    public void DetectCloseCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, detectionLayer);
        bool playerDetectedFar = false;

        foreach (var hitCollider in hitColliders)
        {
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;

            //Shoot out a raycast at player to detect if they are behind a wall or not
            if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, targetCloseDetection))
            {
                Debug.DrawRay(transform.position, directionToTarget * targetCloseDetection, Color.red, 1f);

                //If player is hit and not behind a wall, change state
                if (hit.collider == hitCollider)
                {
                    playerTransform = hitCollider.transform;
                    EvilMarbleSensor.SeeTargetClose = true;
                    EvilMarbleSensor.IsHome = false;
                    playerDetectedFar = true;
                    playerIsDetected = true;
                    EvilRb.velocity = Vector3.zero;
                    break;
                }
            }
        }

        //If player leaves the circle reset back to first state
        if (!playerDetectedFar && playerIsDetected)
        {
            ResetStates();
        }
    }

    public void ResetStates()
    {
        EvilMarbleSensor.SeeTargetClose = false;
        EvilMarbleSensor.IsAttacking = false;
        playerIsDetected = false;
    }

    //show radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);
    }
}
