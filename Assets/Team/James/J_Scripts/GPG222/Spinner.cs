using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

namespace JamesKilpatrick
{
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
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Players should not have control of changing states
            //if (IsServer)
            {
                if (state == states.Idle)
                {
                    rend.material.color = Color.white;
                    // Do Idle
                }
                if (state == states.Rotating)
                {

                    //Starts spin
                    SpinRotate_ServerToClients_RPC();
                    rend.material.color = Color.green;
                }
                if (state == states.Moving)
                {

                    //Starts movement 
                    SpinMovement_ServerToClients_RPC();
                    rend.material.color = Color.red;
                }
            }

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
        [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
        private void SpinMovement_ServerToClients_RPC()
        {
            if (targetObject)
            {
                targetDirection = (targetObject.position - transform.position).normalized;
            }
            else
            {
                targetDirection = (targetPosition - transform.position).normalized;
            }

            //need to create code for when target position is reached to go to new position

            if (reachedTargetPosition == true)
            {
                targetPosition = newTargetPosition;
            }

            float angle = Vector3.SignedAngle(transform.forward, targetDirection, transform.up) * turnspeed;

            spinBody.AddRelativeTorque(0, angle, 0);
            spinBody.AddRelativeForce(0, 0, speed);
        }

        // Function that runs from the Server TO ALL clients
        [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
        public void SpinRotate_ServerToClients_RPC()
        {
            spinTransform.Rotate(0, 1f, 0);
        }
    }
}