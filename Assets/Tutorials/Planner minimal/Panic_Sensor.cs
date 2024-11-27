using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public enum Panic
{
	playerIsNear = 0,
	isCalm = 1
}

public class Panic_Sensor : MonoBehaviour, ISense
{
	public bool seePlayer;
	
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.Set(Panic.playerIsNear, seePlayer);
		aWorldState.Set(Panic.isCalm, false);
	}
}
