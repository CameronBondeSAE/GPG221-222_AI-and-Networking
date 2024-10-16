using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Spinner : MonoBehaviour
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
            // StartCoroutine(Spinning());
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (state == states.Idle)
            {
                rend.material.color = Color.white;
                // Do Idle
            }
            if (state == states.Rotating)
            {
                //Starts spin coroutine
                StartCoroutine(Spinning());
                rend.material.color = Color.green;
            }
            if (state == states.Moving)
            {
                //Starts movement coroutine
                StartCoroutine(Movement());
                rend.material.color = Color.red;
            }

        }

        IEnumerator Spinning()
        {
            spinTransform.Rotate(0, 1f, 0);
            yield return new WaitForSeconds(5f);
            StopCoroutine(Spinning());
            state = states.Moving;
           

        }

        IEnumerator Movement()
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

            yield return new WaitForSeconds(10f);
            StopCoroutine(Movement());
            state = states.Rotating;
            

        }

        IEnumerator StateManager()
        {
            state = states.Rotating;
            yield return new WaitForSeconds(5f);
            state = states.Moving;
        }
    }
}