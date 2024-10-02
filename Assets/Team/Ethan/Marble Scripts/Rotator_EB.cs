using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Rotator_EB : NetworkBehaviour
{
    
    // Variables
    // This links your code to any other code in the whole game. 
    // In this case it's like a wire that can hook in to a "Transform" component script
    public Transform myTransform;

    public float speed;


    // Functions
    // This one gets called by Unity itself, 60 times a second
    void FixedUpdate()
    {
        if (IsServer)
        {
            // Now we've wired it up, we can tell it to do.. whatever it can do! Note the dot
            myTransform.Rotate(0, speed, 0);
        }
        
    }


}
