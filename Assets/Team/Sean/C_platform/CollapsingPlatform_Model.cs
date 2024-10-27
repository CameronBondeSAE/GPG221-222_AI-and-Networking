using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CollapsingPlatform_Model : NetworkBehaviour
{

    public float activateTimer = 5f;
    private float cdTimer;
    public bool timerCalled = false;
    public bool debugTimerFinished = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        cdTimer = activateTimer;
        rb = GetComponent<Rigidbody>();
    }

    // Function that ONLY runs on the server. Typically for client controller code when they press buttons etc
    [Rpc(SendTo.Server, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
    private void Trigger_RequestToServer_Rpc()
    {
        //made bool true on server only
        timerCalled = true;
        Debug.Log("Timer Started");
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
    public void ActivateRPC()
    {
        //Makes platform dissapear
        Debug.Log("Platform activated");
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
           
    }

    public void Timer()
    {
        if (timerCalled == true)
        {     
            if (cdTimer < 0)
            {
                timerCalled = false;
                debugTimerFinished = true;

            }
        }
    }

    public void Update()
    {
        if (IsClient)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Button Pressed");
                Trigger_RequestToServer_Rpc();
            }
        }

        Timer();

        //Debugs after 5 seconds of timer starting, finishing timer which calls only once instead of constantly
        if (debugTimerFinished == true)
        {
            Debug.Log("Timer Finished");
            ActivateRPC();
            debugTimerFinished = false;
        }

    }



}

/*
 * > Activation condition : When player enters trigger box
 * > Request to server (Sendto.Server with owndership)
 * > Answer from server (Sendto.ClientsandHost, no ownership
*/
