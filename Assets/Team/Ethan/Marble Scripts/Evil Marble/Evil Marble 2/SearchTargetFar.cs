using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTargetFar : MonoBehaviour
{
    public float targetCloseDetection = 5f;
    public LayerMask detectionLayer;
    public Transform playerTransform;
    EvilMarbleSensorss EvilMarbleSensorss;

    public void Start()
    {
        EvilMarbleSensorss = GetComponent<EvilMarbleSensorss>();
    }

    void Update()
    {
        DetectCloseCircle();
    }

    public void DetectCloseCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, detectionLayer);

        foreach (var hitCollider in hitColliders)
        {
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, targetCloseDetection))
            {
                Debug.DrawRay(transform.position, directionToTarget * targetCloseDetection, Color.green, 1f);

                if (hit.collider == hitCollider)
                {
                    playerTransform = hitCollider.transform;
                    EvilMarbleSensorss.SeeTargetClose = true;
                    EvilMarbleSensorss.IsHome = false;
                    break;
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);
    }
}
