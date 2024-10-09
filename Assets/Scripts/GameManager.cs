using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : NetworkBehaviour
{
	public int score;
	
	private void Update()
	{
		if (IsServer)
		{
			WaveTimer.Value = (int) Time.timeSinceLevelLoad;

			// HACK: Fake test
			// if (Random.Range(0, 60) == 0)
			// {
			// 	score = score + 1;
			// 	UpdateHighscoreRpc(score);
			// }
		}
	}

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	

	public EndGoal EndGoal;


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

	public NetworkVariable<int> WaveTimer;

	public NetworkVariable<bool> ChestOpenState;


	[Rpc(SendTo.ClientsAndHost, Delivery = RpcDelivery.Reliable)]
	public void UpdateHighscoreRpc(int score)
	{
		Debug.Log(score);
	}
}