using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class SearchTargetFar : MonoBehaviour
{
    // Variables for detection radius, layer, and player transform
    public float targetCloseDetection = 5f;
    public List<LayerMask> detectionLayers;
    public Transform playerTransform;
    EvilMarbleSensorss evilMarbleSensors;
    public Rigidbody EvilRB;
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

    public void Start()
    {
        evilMarbleSensors = GetComponent<EvilMarbleSensorss>();
        EvilRB = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        DetectCloseCircle();
        UpdateColor();


    }

    // Uses a circle collider to detect the player using a layer
    public void DetectCloseCircle()
    {
        foreach (var layer in detectionLayers)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, layer);
            bool playerDetectedFar = false;

            foreach (var hitCollider in hitColliders)
            {
                Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;

                // Raycast to ensure the player is not behind a wall
                if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, targetCloseDetection))
                {
                    Debug.DrawRay(transform.position, directionToTarget * targetCloseDetection, Color.green, 1f);

                    // If player is hit and not obstructed, update detection state
                    if (hit.collider == hitCollider)
                    {
                        playerTransform = hitCollider.transform;
                        detectionMeter += Time.deltaTime;

                        detectionMeter = Mathf.Min(detectionMeter, detectPlayer);

                        if (detectionMeter >= detectPlayer)
                        {
                            evilMarbleSensors.SeeTargetClose = true;
                            evilMarbleSensors.IsHome = false;
                            playerDetectedFar = true;
                            playerIsDetected = true;
                            EvilRB.velocity = Vector3.zero;

                            // Broadcast the detected player location
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

            // Reset state if the player is no longer detected
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
            evilMarbleSensors.IsListening = true;
        }
    }

    private void BroadcastPlayerLocation(Vector3 location)
    {
        Collider[] nearbyAIs = Physics.OverlapSphere(transform.position, broadcastRadius, aiLayer);

        foreach (var ai in nearbyAIs)
        {
            if (ai.TryGetComponent(out SearchLocation_State locationState))
            {
                locationState.ReceiveLocation(location);
            }
        }
    }


    public void ResetStates()
    {
        evilMarbleSensors.SeeTargetClose = false;
        evilMarbleSensors.IsBlocking = false;
        playerIsDetected = false;
        detectionMeter = 0f;
    }

    public void UpdateColor()
    {
        if (!evilMarbleSensors.SeeTargetClose && evilMarbleSensors.IsHome)
        {
            // Lerp between green (0 detection) and red (full detection)
            Color color = Color.Lerp(Color.green, Color.red, detectionMeter / detectPlayer);
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
        
    }

    // Show radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, broadcastRadius);
    }
}
