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
    public SearchTargetFar searchTargetFar;
    private bool hasBroadcastedPlayerPosition = false;

    private bool playerIsDetected;

    // Sound detection variables
    public float soundDetectionRadius = 10f;
    public float broadcastRadius = 15f;
    public LayerMask aiLayer;
    public Vector3 soundLocation;


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
                            hasBroadcastedPlayerPosition = true;
                            break;
                        }

                    }
                }

                if (!playerDetectedFar && playerIsDetected)
                {
                    ResetStates();
                }

            }

            

        }


    }

    public void HearSound(Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, playerPosition) <= soundDetectionRadius)
        {
            soundLocation = playerPosition; // Update the target location
            EvilMarbleSensor.IsListening = true; // AI is now in a listening state
        }
    }

    public void BroadcastPlayerLocation(Vector3 playerPosition)
    {
        // Get all nearby AIs within the broadcast radius
        Collider[] nearbyAIs = Physics.OverlapSphere(transform.position, broadcastRadius, aiLayer);

        foreach (var ai in nearbyAIs)
        {
            // Prevent broadcasting to itself
            if (ai != this.GetComponent<Collider>())
            {
                // Check for SearchPlayerClose component and call HearSound
                if (ai.TryGetComponent(out SearchPlayerClose otherCloseAI))
                {
                    otherCloseAI.HearSound(playerPosition);
                }

                // Check for SearchTargetFar component and call HearSound
                if (ai.TryGetComponent(out SearchTargetFar otherFarAI))
                {
                    otherFarAI.HearSound(playerPosition);
                }
            }
        }
    }

    private void CheckSoundLocationReached()
    {
        if (EvilMarbleSensor.IsListening)
        {
            // Check the distance to the sound location
            float distanceToSound = Vector3.Distance(EvilRb.position, soundLocation);

            if (distanceToSound < 1f)
            {
                ResetStates();
            }
        }
    }

    public void ResetStates()
    {
        EvilMarbleSensor.SeeTargetClose = false;
        EvilMarbleSensor.IsAttacking = false;
        EvilMarbleSensor.IsListening = false;
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
