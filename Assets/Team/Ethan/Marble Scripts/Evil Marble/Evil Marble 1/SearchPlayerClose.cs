using EB;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SearchPlayerClose : MonoBehaviour
{
    //variables for dection layer/radius and to get the players transform
    public float targetCloseDetection = 5f;
    public List<LayerMask> detectionLayers;
    public Transform playerTransform;
    EvilMarbleSensor EvilMarbleSensor;
    public Rigidbody EvilRb;
    public float detectionMeter = 0f;
    public float detectPlayer = 5f;
    public float detectionRate = 2f;
    public Renderer renderer;

    private bool playerIsDetected;

    // Sound detection variables
    public float soundDetectionRadius = 10f;
    public float broadcastRadius = 15f;
    public LayerMask aiLayer;
    private Vector3 soundLocation;
    private bool isMovingToSound = false;


    public void Start()
    {
        EvilMarbleSensor = GetComponent<EvilMarbleSensor>();
        EvilRb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        DetectCloseCircle();
        UpdateColour();
    }

    //Uses a circle collider to detect the player by using a layer
    public void DetectCloseCircle()
    {
        foreach (var layer in detectionLayers)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, layer);
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
                        detectionMeter += Time.deltaTime;

                        detectionMeter = Mathf.Min(detectionMeter, detectPlayer);

                        if (detectionMeter >= detectPlayer)
                        {

                            EvilMarbleSensor.SeeTargetClose = true;
                            EvilMarbleSensor.IsHome = false;
                            playerDetectedFar = true;
                            playerIsDetected = true;
                            EvilRb.velocity = Vector3.zero;
                            BroadcastPlayerLocation(playerTransform.position);
                            break;
                        }

                    }
                }
            }

            if (!playerDetectedFar && playerIsDetected)
            {
                detectionMeter -= Time.deltaTime * detectionRate;
                detectionMeter = Mathf.Max(detectionMeter, 0f);
            }

            //If player leaves the circle reset back to first state
            if (detectionMeter == 0f && playerIsDetected)
            {
                ResetStates();
            }
        }

        

        

       
    }

    public void HearSound(Vector3 soundLocation)
    {
        if (Vector3.Distance(transform.position, soundLocation) <= soundDetectionRadius)
        {
            this.soundLocation = soundLocation;
            isMovingToSound = true;
            EvilMarbleSensor.IsListening = true;
        }
    }

    private void BroadcastPlayerLocation(Vector3 playerPosition)
    {
        Collider[] nearbyAIs = Physics.OverlapSphere(transform.position, broadcastRadius, aiLayer);

        foreach (var ai in nearbyAIs)
        {
            if (ai.TryGetComponent(out SearchPlayerClose otherAI))
            {
                otherAI.HearSound(playerPosition);
            }
        }
    }

    private void MoveToSound()
    {
        Vector3 targetDir = (soundLocation - transform.position).normalized;
        EvilRb.AddForce(targetDir * 10f);

        // Stop moving after reaching the sound location
        if (Vector3.Distance(transform.position, soundLocation) < 1f)
        {
            isMovingToSound = false;
        }
    }

    public void ResetStates()
    {
        EvilMarbleSensor.SeeTargetClose = false;
        EvilMarbleSensor.IsAttacking = false;
        playerIsDetected = false;
        detectionMeter = 0f; 
    }

    public void UpdateColour()
    {
        if (!EvilMarbleSensor.SeeTargetClose && EvilMarbleSensor.IsHome)
        {
            // Lerp between green (0 detection) and red (full detection)
            Color color = Color.Lerp(Color.green, Color.red, detectionMeter / detectPlayer);
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }

    }


    //show radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, broadcastRadius);
    }
}
