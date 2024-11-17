using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamesKilpatrick;


public class EvilMarbleDetection : MonoBehaviour
{
    public float detectionRange = 5f;
    public LayerMask detectionLayer;
    public Transform playerTransform;
    EvilMarbleSensor EvilMarbleSensor;
    public bool isPlayerNear;
    public bool isPlayerDetected;

    void Start()
    {
        EvilMarbleSensor = GetComponent<EvilMarbleSensor>();
    }

    void Update()
    {
        DetectionCircle();
    }

    //Evil Marble Detection Range.
    public void DetectionCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);
        isPlayerNear = false;

        foreach (var hitCollider in hitColliders)
        {
            Vector3 directionToMarble = (hitCollider.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, directionToMarble, out RaycastHit hit, detectionRange))
            {
                Debug.DrawRay(transform.position, directionToMarble * detectionRange, Color.red, 1f);

                //If detecting Player Marble set Chasing to true.
                if (hit.collider == hitCollider)
                {
                    playerTransform = hitCollider.transform;
                    EvilMarbleSensor.SeesPlayer = true;
                    EvilMarbleSensor.IsHome = false;
                    isPlayerDetected = true;
                    isPlayerNear = true;
                    break;
                }
            }
        }

        //If player is detected by gets away reset state to go home
        if (isPlayerNear == false && isPlayerDetected == true)
        {
            ResetDetection();
        }
    }

    //Resets to home state and allows for ResetDetection to work again
    public void ResetDetection()
    {
        EvilMarbleSensor.SeesPlayer = false;
        EvilMarbleSensor.IsAttacking = false;
        isPlayerDetected = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

