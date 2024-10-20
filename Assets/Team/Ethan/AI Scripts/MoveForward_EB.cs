using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EB
{
    public class MoveForward_EB : MonoBehaviour
    {

        public Rigidbody rb;

        public float speed = 100f;

        public float yourTurnDirAndSpeed = 2;




        // Start is called before the first frame update
        void Start()
        {
            rb.AddRelativeTorque(0, yourTurnDirAndSpeed, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (!Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                rb.AddRelativeForce(0, 0, speed);
            }


            rb.AddRelativeForce(0, 0, speed);


        }






    }
}

