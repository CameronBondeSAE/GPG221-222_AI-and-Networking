using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Avoid : MonoBehaviour
    {
        public Rigidbody rb;
        public float distance = 2f;
        public float sideDistance = 1f;
        public float speed = 100f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // If raycasts are hit character will rotate

            //Forward raycast
            bool hitSomething = Physics.Raycast(origin: transform.position, direction: transform.forward, out RaycastHit hit, distance);
            //Right raycast
            bool hitRightSide = Physics.Raycast(origin: transform.position, direction: transform.right, out RaycastHit hitRight, sideDistance);
            //Left raycast
            bool hitLeftSide = Physics.Raycast(origin: transform.position, direction: -transform.right, out RaycastHit hitLeft, sideDistance);

            //Game logic for when raycasts are hit
            if (hitSomething)
            {
                rb.AddRelativeTorque(0, speed, 0);
                Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
                Debug.Log("Front Hit");
            }          
            if (hitRightSide)
            {
                rb.AddRelativeTorque(0, -speed, 0);
                Debug.DrawRay(transform.position, transform.right * sideDistance, Color.red);
                Debug.Log("Right Hit");
            }
            if(hitLeftSide)
            {
                rb.AddRelativeTorque(0, speed, 0);
                Debug.DrawRay(transform.position, -transform.right * sideDistance, Color.red);
                Debug.Log("Left Hit");
            }

            //Show lines in game mode when gizmos is on
            Debug.DrawRay(transform.position, transform.forward * distance, Color.green);
            Debug.DrawRay(transform.position, transform.right * sideDistance, Color.green);
            Debug.DrawRay(transform.position, -transform.right * sideDistance, Color.green);

        }
    }
}