using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JamesKilpatrick
{
    public class Marble : NetworkBehaviour
    {
        // Variables
        // They store information
        // This one stores which 'Rigidbody' component we want to talk to. They do the physics movement
        public Rigidbody rb;
        // This one stores a 'float' which is just a number. You can change these in the editor
        public float speed = 25f;

        public Vector3 marblePosition;
        public Vector3 marbleVelocity;
        public Vector3 marbleAngularVelocity;
        // public string soundType;
        // public float radius;


        // Functions
        // This gets run by Unity whenever the screen updates
        void Update()
        {
            // We're talking to the Rigidbody via our variable. Note the dot. This will show you everything that component can do
            rb.AddTorque(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            CalculateMarbleStatsServerRPC();
        }

        [Rpc(SendTo.Server)]
        public void CalculateMarbleStatsServerRPC()
        {
            //Calculates current position to use when sending location to server
            marblePosition = transform.position;

            //Calculates current velocity to use when sending location to server
            marbleVelocity = rb.velocity;

            //Caluculates current angularVelocity to use when sending location to server
            marbleAngularVelocity = rb.angularVelocity;

            UpdateMarblePostitionRPC();
        }

        [Rpc(SendTo.ClientsAndHost)]
        public void UpdateMarblePostitionRPC()
        {
            transform.position = marblePosition;
            rb.velocity = marbleVelocity;
            rb.angularVelocity = marbleAngularVelocity;

        }





    }
}
