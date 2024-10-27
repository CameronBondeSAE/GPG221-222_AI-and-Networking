using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class Spinner : NetworkBehaviour
{
    public Rigidbody spinBody;

    //Spins Game Object 
    public Transform spinTransform;

    //The Start Point for the Game Object used for its movement
    public Vector3 targetPosition;

    public Vector3 newTargetPosition;

    //Direction Game Object moves to
    public Vector3 targetDirection;

    //Speed of turn
    public float turnspeed;

    //Speed of Movement
    public float speed;

    //Used to make spinner move towards a Game Object
    public Transform targetObject;

    public Renderer rend;

    public bool reachedTargetPosition;

    //Starting Position of Spinner used too loop movement
    public Vector3 startPosition;

    //Current Position of spinner used too check bool
    public Vector3 currentPosition;

    public enum states
    {
        Idle,
        Rotating,
        Moving
    }
    public states state;

    void Start()
    {
        StartCoroutine(StateManager());
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Players should not have control of changing states
        if (IsServer)
        {
            if (state == states.Idle)
            {
                rend.material.color = Color.white;
                // Do Idle
            }
            if (state == states.Rotating)
            {
                //Starts spin
                SpinRotate();
                ChangeColourToGreen_Rpc();
            }
            if (state == states.Moving)
            {
                //Starts movement 
                SpinMovement();
                ChangeColourToRed_Rpc();
            }
        }
        TargetPositionChanger();
        currentPosition = transform.position;
    }
    IEnumerator StateManager()
    {
        state = states.Rotating;
        yield return new WaitForSeconds(5);
        state = states.Moving;
        yield return new WaitForSeconds(10);
        Debug.Log("Endo");
        StartCoroutine(StateManager());
    }

    // Function that runs from the Server TO ALL clients
    private void SpinMovement()
    {
        //Used to move towards player marble if wanted
        if (targetObject)
        {
            targetDirection = (targetObject.position - transform.position).normalized;
        }
        //Moves towards a position
        else
        {
            targetDirection = (targetPosition - transform.position).normalized;
        }

        float angle = Vector3.SignedAngle(transform.forward, targetDirection, transform.up) * turnspeed;

        spinBody.AddRelativeTorque(0, angle, 0);
        spinBody.AddRelativeForce(0, 0, speed);
    }

    // Function that runs from the Server TO ALL clients
    public void SpinRotate()
    {
        spinTransform.Rotate(0, 1f, 0);
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
    public void ChangeColourToRed_Rpc()
    {
        //Changes object to red when moving
        rend.material.color = Color.red;
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
    public void ChangeColourToGreen_Rpc()
    {
        //Changes object to green when spinning
        rend.material.color = Color.green;
    }

    public void TargetPositionChanger()
    {
        //Loops Position. this allows for longer routes with the same code
        if (currentPosition == newTargetPosition)
        {
            reachedTargetPosition = true;
        }
        if (currentPosition == startPosition)
        {
            reachedTargetPosition = false;
        }


        if (reachedTargetPosition == true)
        {
            targetPosition = startPosition;
        }
        if (reachedTargetPosition == false)
        {
            targetPosition = newTargetPosition;
        }
    }
}
