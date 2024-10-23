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
            bool hitSomething = Physics.Raycast(origin: transform.position, direction: transform.forward, out RaycastHit hit, distance); /*|| Physics.Raycast(origin: transform.position, direction: transform.right, out RaycastHit hitRight, sideDistance) || Physics.Raycast(origin: transform.position, direction: -transform.right, out RaycastHit hitLeft, sideDistance);*/
            bool hitRightSide = Physics.Raycast(origin: transform.position, direction: transform.right, out RaycastHit hitRight, sideDistance);
            bool hitLeftSide = Physics.Raycast(origin: transform.position, direction: -transform.right, out RaycastHit hitLeft, sideDistance);

            if (hitSomething)
            {
                rb.AddRelativeTorque(0, speed, 0);
            }          
            if (hitRightSide)
            {
                rb.AddRelativeTorque(0, speed, 0);
            }
            if(hitLeftSide)
            {
                rb.AddRelativeTorque(0, speed, 0);
            } 
            
        }
    }
}