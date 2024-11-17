using EB;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block_State : EvilMarbleBase
{
    //References to other gameobjects/scripts
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public SearchPlayerClose searchPlayerClose;
    public TextFaceCameraEvilMarble textEvilMarble;
    private Rigidbody playerRb; 
    private Transform playerTransform;

    //make sure when created to get all references
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();

        //get players transfom
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            playerTransform = player.transform;
        }
    }

    //when entering the state change colour and txt
    public override void Enter()
    {
        base.Enter();
        EvilMarbleBase.marbleRenderer.material.color = Color.gray;
        textEvilMarble.GetComponent<TMP_Text>().text = "Block State";
    }

    //Check players velocity to move to suitable block location
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (playerRb == null || playerTransform == null)
        {
            return;
        }
        //get block location
        if (EvilMarbleBase.currentBlockTarget == null || Vector3.Distance(transform.position, EvilMarbleBase.currentBlockTarget.position) < EvilMarbleBase.targetReachThreshold)
        {
            //if at block location, turn velocity to 0 so we don't fly away
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

            //Get the block location depending on player
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

        //go to block location
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
