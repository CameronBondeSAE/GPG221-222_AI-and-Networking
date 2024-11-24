using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// This script manages the end game and sets off events when it does. It manages the txt that show up on screen when the endgame is triggered 
/// checking who won and lost depending on who triggered it
/// </summary>
public class GameManager_EB : NetworkBehaviour
{
    [Tooltip("EndGoal object/script")]
    [SerializeField] private EndGoal_EB EndGoal;
    [Tooltip("Win txt to display")]
    [SerializeField] private GameObject winTxt;
    [Tooltip("Lose txt to display")]
    [SerializeField] private GameObject lostTxt;

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

