using Anthill.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Netcode;
using UnityEngine;

public class Basher_EB : NetworkBehaviour
{
    // basher
    [SerializeField] private GameObject basher;

    // timers for basher, can change in inspecter 
    [SerializeField] private float startTimer;
    [SerializeField] private float downTimer;
    [SerializeField] private float upTimer;

    // base speed, can change in inspecter 
    [SerializeField] private float speed = 10;

    // Basher positions
    [SerializeField] private Transform startPostion, endPostion;
    [SerializeField] private Transform destinationTarget, departTarget;
    //change delay
    [SerializeField] float changeDirectionDelay;
    //length of journey
    private float journeyLength;
    //bool for waiting
    private bool isWaiting;
    //render to change colour
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {

        //Sets target position
        departTarget = startPostion;
        destinationTarget = endPostion;

        //starts timer
        startTimer = Time.time;
        // gets its journey distance
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        //starts basher
        isWaiting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if is server start moving
       if (IsServer)
            MoveDown();
    }

    private void MoveDown()
    {
        if (!isWaiting)
        {
            //if distance is greater then 0.01 call
            if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                //get basher speed
                float distCovered = (Time.time - startTimer) * speed;
                
                float fractionOfJourney = distCovered / journeyLength;
                //move to destination
                transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
                // change colour on server
                if (IsServer && !isWaiting)
                {

                    ChangeColourBlue_RPC();
                }
            }
            else
            {
                isWaiting = true;
                StartCoroutine(changeDeley());
            }
        }
    }

    private void ChangeDestination()
    {
        //reset postions to new positions
        if (departTarget == endPostion && destinationTarget == startPostion)
        {
            departTarget = startPostion;
            destinationTarget = endPostion;
        }
        else 
        {
            departTarget = endPostion;
            destinationTarget = startPostion;
        }
    }
    IEnumerator changeDeley()
    {
        //change to new postion and get ready to move
        yield return new WaitForSeconds(changeDirectionDelay);
        ChangeDestination();
        startTimer = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }

    [Rpc(SendTo.Server, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
    private void ChangeColourBlue_RPC()
    {
        // change colour to blue
        rend.material.color = Color.blue;
    }

}
