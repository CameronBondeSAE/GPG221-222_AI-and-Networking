using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager_EB : NetworkBehaviour
{
    public EndGoal_EB EndGoal;

    private void OnEnable()
    {
        EndGoal.EndGoalReached_Event += EndGoalOnEndGoalReached_Event;
    }


    private void OnDisable()
    {
        EndGoal.EndGoalReached_Event -= EndGoalOnEndGoalReached_Event;
    }


    private void EndGoalOnEndGoalReached_Event()
    {
        Debug.Log("Game over");
    }
}

