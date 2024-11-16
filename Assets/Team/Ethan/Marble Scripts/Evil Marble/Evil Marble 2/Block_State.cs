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

    private Rigidbody playerRb; 
    private Transform playerTransform;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();

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
            return;
        }

        if (EvilMarbleBase.currentBlockTarget == null || Vector3.Distance(transform.position, EvilMarbleBase.currentBlockTarget.position) < EvilMarbleBase.targetReachThreshold)
        {
            if (EvilMarbleBase.currentBlockTarget != null)
            {
                EvilRB.velocity = Vector3.zero;
                return;
            }

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
                Vector3 direction = (EvilMarbleBase.currentBlockTarget.position - EvilRB.position).normalized;
                Vector3 newPosition = EvilRB.position + direction * EvilMarbleBase.movementSpeed * Time.deltaTime;

                EvilRB.MovePosition(newPosition);
            }
            else
            {
                EvilRB.velocity = Vector3.zero;
                EvilMarbleBase.currentBlockTarget = null;
            }
        }
    }
}
