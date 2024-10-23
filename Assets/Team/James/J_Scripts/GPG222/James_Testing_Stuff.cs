using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JamesKilpatrick
{
    public class James_Testing_Stuff : NetworkBehaviour
    {
        //Testing space to understand code and not mess anything else up.

        /// <summary>
        /// This Following Section is about sending functions from the client to the server and functions sending back to all clients.
        /// Key Concepts to remember is:
        /// - Make sure Client is not able to abuse SendTo.Server Functions.
        /// - Make sure Clients send requests instead of Functions.
        /// </summary>

        //When sending to server make sure Ownership is true so it cannot be abused.
        [Rpc(SendTo.Server, RequireOwnership = true, Delivery = RpcDelivery.Unreliable)]

        //Have a Client when sending to server is requesting for a function to happen instead of just telling the server to preform function.
        public void GoToServerRequest_Rpc()
        {

        }

        //Function that runs from the server to all clients.
        [Rpc(SendTo.NotServer, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]

        //This is where change that affects all clients should happen. Making it another void allows for the function to happen but also for clients to not be able to abuse this function as it's protected by server access.
        public void ServerRequestResponse_Rpc()
        {

        }
        public void Update()
        {

        }

    }
}
