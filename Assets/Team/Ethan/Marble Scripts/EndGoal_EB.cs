using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EndGoal_EB : NetworkBehaviour
{

    public delegate void Simple(ulong winClientId);


    public event Simple EndGoalReached_Event;

    [Tooltip("Bool so it only triggers once")]
    public bool gameOver = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsServer && !gameOver)
        {
            var player = other.GetComponent<NetworkObject>();

            if (player != null)
            {
                ulong winClientId = player.OwnerClientId;

                gameOver = true;

                EndGoalReached_Event?.Invoke(winClientId);
            }
        }
        
    }

}
