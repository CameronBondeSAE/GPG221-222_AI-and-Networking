using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JamesKilpatrick
{
    /// <summary>
    /// James's Player Marble
    /// TODO:
    /// Implement special ability to seperate it from base class (Jumping ability)
    /// 
    /// This marble networks its position, velocity and angular velocity to allow all players to see this marble in their clients allow it to be customised more than what regular network transform allows
    /// It uses torque on its rigidbody to move around
    /// </summary>
    public class Marble : NetworkBehaviour
    {
        // Variables
        // They store information
        [Tooltip("This one stores which 'Rigidbody' component we want to talk to. They do the physics movement")]
        public Rigidbody rb;
        [Tooltip("This one stores a 'float' which is just a number. You can change these in the editor")]
        public float speed = 25f;

        private Vector3 marblePosition;
        private Vector3 marbleVelocity;
        private Vector3 marbleAngularVelocity;

        // Functions

        private void Start()
        {
            //Calculates current position to use when sending location to server
            marblePosition = transform.position;

            //Calculates current velocity to use when sending location to server
            marbleVelocity = rb.velocity;

            //Caluculates current angularVelocity to use when sending location to server
            marbleAngularVelocity = rb.angularVelocity;
        }

        // This gets run by Unity whenever the screen updates
        void Update()
        {
            if (IsOwner)
            {
                // We're talking to the Rigidbody via our variable. Note the dot. This will show you everything that component can do
                rb.AddTorque(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
               
                CalculateMarbleStatsServerRPC(transform.position, rb.velocity, rb.angularVelocity);
            }
        }

        [Rpc(SendTo.Server, RequireOwnership = false)]
        private void CalculateMarbleStatsServerRPC(Vector3 position, Vector3 velocity, Vector3 angularVelocity)
        {
            //Calculates current position to use when sending location to server
            transform.position = position;

            //Calculates current velocity to use when sending location to server
            rb.velocity = velocity;

            //Caluculates current angularVelocity to use when sending location to server
            rb.angularVelocity = angularVelocity;

            UpdateMarblePostitionRPC(position, velocity, angularVelocity);
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void UpdateMarblePostitionRPC(Vector3 position, Vector3 velocity, Vector3 angularVelocity)
        {
            if (!IsOwner)
            {
                transform.position = position;
                rb.velocity = velocity;
                rb.angularVelocity = angularVelocity;
            }

        }

    }
}
