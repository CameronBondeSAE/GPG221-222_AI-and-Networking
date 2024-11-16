using EB;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SearchPlayerClose : MonoBehaviour
{
    public float targetCloseDetection = 5f;
    public LayerMask detectionLayer;
    public Transform playerTransform;
    EvilMarbleSensor EvilMarbleSensor;


    public void Start()
    {
        EvilMarbleSensor = GetComponent<EvilMarbleSensor>();
    }
    void Update()
    {
        DetectCloseCircle();
    }

    public void DetectCloseCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, detectionLayer);
        //bool playerDetectedFar = false;

        foreach (var hitCollider in hitColliders)
        {
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, targetCloseDetection))
            {
                Debug.DrawRay(transform.position, directionToTarget * targetCloseDetection, Color.red, 1f);

                if (hit.collider == hitCollider)
                {
                    playerTransform = hitCollider.transform;
                    EvilMarbleSensor.SeeTargetClose = true;
                    EvilMarbleSensor.IsHome = false;
                    //playerDetectedFar = true;
                    break;
                }
            }
        }

        //if (!playerDetectedFar)
        //{
        //    EvilMarbleSensor.SeeTargetClose = false;
        //    EvilMarbleSensor.IsAttacking = false;
        //    playerTransform = null;
        //}
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);
    }
}
