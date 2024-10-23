using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Netcode;
using UnityEngine;

public class Basher_EB : NetworkBehaviour
{
    [SerializeField] private GameObject basher;
    [SerializeField] private float startTimer;
    [SerializeField] private float downTimer;
    [SerializeField] private float upTimer;
    [SerializeField] private float speed = 10;
    [SerializeField] private Transform startPostion, endPostion;
    [SerializeField] private Transform destinationTarget, departTarget;
    [SerializeField] float changeDirectionDelay;
    private float journeyLength;
    private bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        departTarget = startPostion;
        destinationTarget = endPostion;

        startTimer = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (IsServer)
            MoveDown();
    }

    private void MoveDown()
    {
        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                float distCovered = (Time.time - startTimer) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
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
        yield return new WaitForSeconds(changeDirectionDelay);
        ChangeDestination();
        startTimer = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }

}
