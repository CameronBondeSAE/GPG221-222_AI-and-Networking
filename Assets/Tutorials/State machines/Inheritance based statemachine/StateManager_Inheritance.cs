using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager_Inheritance : MonoBehaviour
{
	public StateBase currentState;

	public void ChangeState(StateBase newState)
	{
		currentState.Exit();
		newState.Enter();
		currentState = newState;
	}

	private void Update()
	{
		currentState.Execute();
	}
}