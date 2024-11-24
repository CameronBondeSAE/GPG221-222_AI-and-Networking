using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MarbleNetworkconnect : NetworkBehaviour
{
    //Variables that need to be collected for the network
    private Vector3 playerPos;
    private Vector3 playerVel;
    private Vector3 playerAngVel;
    

    [SerializeField] private MarbleControllerView marbleView;
    [SerializeField] private MarbleController marbleControl;
    //ref to controllerscript


    // Start is called before the first frame update
    void Awake()
    {

        //marbleView = GetComponent<MarbleControllerView>();
        //marbleControl = GetComponent<MarbleController>();  

        playerPos = transform.position;
        playerVel = marbleControl.GetComponent<Rigidbody>().velocity;
        playerAngVel = marbleControl.GetComponent<Rigidbody>().angularVelocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Grab stats from player script and send to server
        if (IsOwner)
        {
            SendStatsServerRPC();
        }
        
        
        
    }

    //Playerstatistics RPCs

    //Send stats to server
    [ServerRpc(RequireOwnership = false)]
    private void SendStatsServerRPC()
    {
        playerPos = transform.position;
        playerVel = marbleControl.GetComponent<Rigidbody>().velocity;
        playerAngVel = marbleControl.GetComponent<Rigidbody>().angularVelocity;

        GrabStatsClientRpc(playerPos, playerVel, playerAngVel);
    }

    //grab stats from server and send to clients
    [ClientRpc]
    private void GrabStatsClientRpc(Vector3 pos, Vector3 vel, Vector3 avel)
    {
        if (!IsOwner)
        {
            transform.position = pos;
            marbleControl.GetComponent<Rigidbody>().velocity = vel;
            marbleControl.GetComponent<Rigidbody>().angularVelocity = avel;
        }

    }

   

    //JumpRPCs
    [Rpc(SendTo.Server, RequireOwnership = true, Delivery = RpcDelivery.Reliable)]
    public void reqJumpValidationRPC()
    {
        ActivateJumpRPC();
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = true, Delivery = RpcDelivery.Unreliable)]
    void ActivateJumpRPC()
    {
        //Activate the jump stuff for view script
        marbleView.JumpEffects();
    }
}
