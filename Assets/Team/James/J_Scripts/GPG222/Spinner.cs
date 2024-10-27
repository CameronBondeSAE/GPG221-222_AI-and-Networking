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

    //Target postion spinner moves towards when in movement state
    public Vector3 targetDirection;

    //Speed of Movement
    public float speed;

    //Used to make spinner move towards a Game Object
    public Transform targetObject;

    //Used to change the spinners color 
    public Renderer rend;

    //Bool used for looping movement
    public bool goingRight;


    public enum states
    {
        Idle,
        Rotating,
        Alignment,
        Moving
    }
    public states state;

    void Start()
    {
        StartCoroutine(StateManager());
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
            if (state == states.Alignment)
            {
                //Alligns rotation back to set path so it does not move around crazy
                ChangeColourToYellow_Rpc();
                SpinAlign();
            }
            if (state == states.Moving)
            {
                //Starts movement 
                SpinMovement();
                ChangeColourToRed_Rpc();
            }
        }

    }
    IEnumerator StateManager()
    {
        state = states.Rotating;
        yield return new WaitForSeconds(5);
        state = states.Alignment;
        yield return new WaitForSeconds(1);
        state = states.Moving;
        yield return new WaitForSeconds(5);
        Debug.Log("Endo");
        StartCoroutine(StateManager());
    }

    //Alligns Spinner back to target position
    private void SpinAlign()
    {
        //Used to move towards player marble if wanted
        if (targetObject)
        {
            targetDirection = (targetObject.position - transform.position).normalized;
        }

        spinTransform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void SpinMovement()
    {
        if (goingRight == true)
        {
            spinBody.AddRelativeForce(0, 0, speed);
        }
        else
        {
            spinBody.AddRelativeForce(0, 0, -speed);
        }

    }

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

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
    public void ChangeColourToYellow_Rpc()
    {
        //Changes object to Yellow when alligning back to target position
        rend.material.color = Color.yellow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            goingRight = true;
        }
        if (collision.gameObject.CompareTag("RightWall"))
        {
            goingRight = false;
        }
    }
}
