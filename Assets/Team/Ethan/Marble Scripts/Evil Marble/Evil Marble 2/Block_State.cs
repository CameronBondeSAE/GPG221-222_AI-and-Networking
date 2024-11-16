using EB;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block_State : EvilMarbleBase
{
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public SearchPlayerClose searchPlayerClose;
    public TextFaceCameraEvilMarble textEvilMarble;

    private Rigidbody playerRb;  // Player Rigidbody
    private Transform playerTransform;  // Player Transform

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();

        // Find player Rigidbody and Transform at runtime
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            playerTransform = player.transform;
        }
    }

    public override void Enter()
    {
        base.Enter();
        EvilMarbleBase.marbleRenderer.material.color = Color.gray;
        textEvilMarble.GetComponent<TMP_Text>().text = "Block State";
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (playerRb == null || playerTransform == null)
        {
            return; // Exit if player data isn't available
        }

        // Proceed with block logic
        if (EvilMarbleBase.currentBlockTarget == null || Vector3.Distance(transform.position, EvilMarbleBase.currentBlockTarget.position) < EvilMarbleBase.targetReachThreshold)
        {
            if (EvilMarbleBase.currentBlockTarget != null)
            {
                // Stop movement since we reached the target
                EvilRB.velocity = Vector3.zero; // Reset velocity
                return;
            }

            // Check if blockLocations list has entries
            if (EvilMarbleBase.blockLocations.Count == 0)
            {
                return;
            }

            Transform closestBlock = null;
            float closestDistance = Mathf.Infinity;

            foreach (var blockLocation in EvilMarbleBase.blockLocations)
            {
                Vector3 directionToBlock = (blockLocation.position - playerTransform.position).normalized;
                float distance = Vector3.Distance(transform.position, blockLocation.position);

                // Check if the direction to block is aligned with player's movement
                if (Vector3.Dot(directionToBlock, playerRb.velocity.normalized) > 0.5f && distance < closestDistance)
                {
                    closestBlock = blockLocation;
                    closestDistance = distance;
                }
            }

            if (closestBlock != null)
            {
                EvilMarbleBase.currentBlockTarget = closestBlock;
            }
        }

        if (EvilMarbleBase.currentBlockTarget != null)
        {
            float distanceToTarget = Vector3.Distance(EvilRB.position, EvilMarbleBase.currentBlockTarget.position);

            if (distanceToTarget >= EvilMarbleBase.targetReachThreshold)
            {
                // Calculate the new position using Rigidbody's current position
                Vector3 direction = (EvilMarbleBase.currentBlockTarget.position - EvilRB.position).normalized;
                Vector3 newPosition = EvilRB.position + direction * EvilMarbleBase.movementSpeed * Time.deltaTime;

                // Move the Rigidbody
                EvilRB.MovePosition(newPosition);
            }
            else
            {
                // Stop movement when reaching the block location
                EvilRB.velocity = Vector3.zero; // Reset velocity
                EvilMarbleBase.currentBlockTarget = null; // Reset to indicate the block has been reached
            }
        }
    }
}
