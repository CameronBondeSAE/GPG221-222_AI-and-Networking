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

    public void Trigger()
    {
        states = States.Triggered;

        Debug.Log("Platform triggered");
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
    public void Activate_Rpc()
    {
        states = States.Activated;
        Debug.Log("Platform activated");
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (states == States.Triggered) {

            cdTimer -= Time.deltaTime;
        
        }

        if (cdTimer <= 0 && states != States.Activated)
        {
            ReqActivation_Rpc();
        }

        
            if (Input.GetKeyDown(KeyCode.Space)) { Trigger(); }
        
    }

    [Rpc(SendTo.Server, RequireOwnership = true, Delivery = RpcDelivery.Reliable)]
    public void ReqActivation_Rpc()
    {
        Activate_Rpc();
    }
}
