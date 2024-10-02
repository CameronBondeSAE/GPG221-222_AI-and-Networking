using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Rotator : MonoBehaviour
    {
        // Variables
        // This links your code to any other code in the whole game. 
        // In this case it's like a wire that can hook in to a "Transform" component script
        public Transform myTransform;


        // Functions
        // This one gets called by Unity itself, 60 times a second
        void FixedUpdate()
        {
            // Now we've wired it up, we can tell it to do.. whatever it can do! Note the dot
            myTransform.Rotate(0, 1f, 0);
        }

    }
}