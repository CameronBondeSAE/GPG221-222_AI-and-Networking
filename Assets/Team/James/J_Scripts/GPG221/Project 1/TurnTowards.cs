using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace JamesKilpatrick
{
    public class TurnTowards : MonoBehaviour
    {
        public Rigidbody rb;
        public float turnspeed;
        public Vector3 targetPosition;
        public Transform targetObject;

        private void Start()
        {

        }
        private void FixedUpdate()
        {
            Vector3 targetDirection;

            if (targetObject)
            //Structure code 
            {
                //If target exists move towards
                targetDirection = (targetObject.position - transform.position).normalized;
            }
            else
            {
                //If target doesn't exist move towards set position 
                targetDirection = (targetPosition - transform.position).normalized;
            }

            float angle = Vector3.SignedAngle(transform.forward, targetDirection, transform.up) * turnspeed;

            rb.AddRelativeTorque(0, angle, 0);
        }


    }
}