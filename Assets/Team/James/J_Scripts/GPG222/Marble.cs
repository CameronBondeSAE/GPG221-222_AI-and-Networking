using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Marble : MonoBehaviour
    {
        // Variables
        // They store information
        // This one stores which 'Rigidbody' component we want to talk to. They do the physics movement
        public Rigidbody rb;
        // This one stores a 'float' which is just a number. You can change these in the editor
        public float speed = 25f;

        //public Vector3 position;
       // public string soundType;
       // public float radius;


        // Functions
        // This gets run by Unity whenever the screen updates
        void Update()
        {
            // We're talking to the Rigidbody via our variable. Note the dot. This will show you everything that component can do
            rb.AddTorque(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            
        }

       /*
        public void EmitSound()
        {
            Collider[] results = new Collider[] { };

            Physics.OverlapSphereNonAlloc(transform.position, radius, results);

            foreach (Collider result in results)
            {
                EvilMarble evilMarbleSound = result.GetComponent<EvilMarble>();
                if (evilMarbleSound != null)
                {
                    evilMarbleSound.HeardSound();
                }
            }
        }
       */
       

    }
}
