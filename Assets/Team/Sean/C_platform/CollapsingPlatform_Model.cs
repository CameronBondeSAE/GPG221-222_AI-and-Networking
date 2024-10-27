using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Netcode;

public class CollapsingPlatform_Model : NetworkBehaviour
{


    public float activateTimer = 5f;
    private float cdTimer;

    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        cdTimer = activateTimer;
        rb = GetComponent<Rigidbody>();
    }

    public enum States
    {
        Idle, Activated, Triggered
    }

    public States states = States.Idle;

    [Rpc(SendTo.Server, RequireOwnership = true, Delivery = RpcDelivery.Reliable)]
    public void ReqTrigger_Rpc()
    {
        Trigger_Rpc();
    }
    //PLACEHOLDER
    public void Trigger_Rpc()
    {
        states = States.Triggered;

        Debug.Log("Platform triggered");
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
    public void ActivateRPC()
    {
        states = States.Activated;
        Debug.Log("Platform activated");
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsServer)
        {
            if (states == States.Triggered)
            {

                cdTimer -= Time.deltaTime;
                Debug.Log(cdTimer);

            }

            if (cdTimer <= 0)
            {
                ActivateRPC();
            }
        }
        //Placeholder activation

       

    }

    public void Update()
    {
        if (IsLocalPlayer)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReqTrigger_Rpc();
                Debug.Log("space pressed");

            }
        }
    }



}

/*
 * > Activation condition : When player enters trigger box
 * > Request to server (Sendto.Server with owndership)
 * > Answer from server (Sendto.ClientsandHost, no ownership
*/
