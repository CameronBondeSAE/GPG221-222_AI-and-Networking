using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    public bool seeTargetFar;
    public bool seeTargetClose;
    public bool pathlocation;
    public bool block;
    public bool attacking;

    public List<Transform> blockLocations = new List<Transform>();

    public float targetFarDetection = 10f;
    public float targetCloseDetection = 5f;
    public LayerMask detectionLayer;

    private Transform currentBlockTarget;
    public Transform playerTransform;
    public Transform playerTransformClose;
    public Rigidbody player_rb;

    public float movementSpeed = 5f;
    public float chaseSpeed = 5f;
    public float targetReachThreshold = 0.5f;

    // Update is called once per frame
    void Update()
    {
        DetectFarCircle();
        DetectCloseCircle();

        if (seeTargetClose && playerTransform != null) // Check if close detection is true
        {
            ChasePlayer(); // Call ChasePlayer if the player is within the close detection circle
        }
        else if (playerTransform != null)
        {
            MoveToBlockLocation(); // Move to block location if the player is not in the close detection circle
        }
    }


    public void DetectFarCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetFarDetection, detectionLayer);
        bool playerDetectedFar = false;

        foreach (var hitCollider in hitColliders)
        {
            playerTransform = hitCollider.transform;
            player_rb = hitCollider.GetComponent<Rigidbody>(); // Get the Rigidbody component to access velocity
            Debug.Log("Player detected in far circle");
            seeTargetFar = true;
            playerDetectedFar = true;
        }

        if (!playerDetectedFar)
        {
            seeTargetFar = false;
            playerTransform = null;
            player_rb = null;
        }

        
    }

    public void DetectCloseCircle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetCloseDetection, detectionLayer);
        bool playerDetectedClose = false;

        foreach (var hitCollider in hitColliders)
        {
            playerTransform = hitCollider.transform;
            seeTargetClose = true;
            playerDetectedClose = true;
        }

        if (!playerDetectedClose)
        {
            seeTargetClose = false;
        }
    }

    // Visualize the detection spheres in the Scene view
    private void OnDrawGizmosSelected()
    {
        // Set the color and draw the far detection circle
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetFarDetection);

        // Set the color and draw the close detection circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetCloseDetection);
    }

    private void MoveToBlockLocation()
    {
        if (player_rb == null) return;

        // Check if we have a current block target
        if (currentBlockTarget == null || Vector3.Distance(transform.position, currentBlockTarget.position) < targetReachThreshold)
        {
            // If we reached the target, clear it to find a new one
            currentBlockTarget = null;

            Vector3 playerVelocity = player_rb.velocity;
            Transform closestBlock = null;
            float closestDistance = Mathf.Infinity;

            // Find the block location closest to the player's velocity direction
            foreach (var blockLocation in blockLocations)
            {
                Vector3 directionToBlock = (blockLocation.position - playerTransform.position).normalized;
                float distance = Vector3.Distance(transform.position, blockLocation.position);

                // Check if this block aligns with the player's velocity direction and is closer than previous ones
                if (Vector3.Dot(directionToBlock, playerVelocity.normalized) > 0.5f && distance < closestDistance)
                {
                    closestBlock = blockLocation;
                    closestDistance = distance;
                }
            }

            // Set the closest block location as the target if found
            if (closestBlock != null)
            {
                currentBlockTarget = closestBlock;
                Debug.Log("New block location target: " + currentBlockTarget.name);
            }
        }



        // Move towards the current block target if it's set
        if (currentBlockTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentBlockTarget.position, Time.deltaTime * movementSpeed);
            Debug.Log("Moving towards block location: " + currentBlockTarget.name);
        }
    }

    private void ChasePlayer()
    {
        if (seeTargetClose == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * chaseSpeed);
            Debug.Log("Chasing player...");
        }
    }

}
