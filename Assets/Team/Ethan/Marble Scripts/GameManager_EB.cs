using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager_EB : NetworkBehaviour
{
    [Tooltip("EndGoal object/script")]
    public EndGoal_EB EndGoal;
    [Tooltip("Win txt to display")]
    public GameObject winTxt;
    [Tooltip("Lose txt to display")]
    public GameObject lostTxt;

    private void OnEnable()
    {
        EndGoal.EndGoalReached_Event += EndGoalOnEndGoalReached_Event;
    }


    private void OnDisable()
    {
        EndGoal.EndGoalReached_Event -= EndGoalOnEndGoalReached_Event;
    }


    private void EndGoalOnEndGoalReached_Event(ulong winClientId)
    {
        Debug.Log("Game over");
        if (IsServer)
        {
            EndGameClientRpc(winClientId);
        }
    }

    [ClientRpc]
    private void EndGameClientRpc(ulong winClientId)
    {
        if (NetworkManager.Singleton.LocalClientId == winClientId)
        {
            winTxt.SetActive(true);
        }
        else
        {
            lostTxt.SetActive(true);
        }
    }
}

