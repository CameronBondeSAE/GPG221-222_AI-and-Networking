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

        foreach (var hitCollider in hitColliders)
        {
            // put raycast in to check if we can see the player or is their a wall in the way
            playerTransform = hitCollider.transform;
            EvilMarbleSensor.SeeTargetClose = true;
            EvilMarbleSensor.IsHome = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);
    }
}
