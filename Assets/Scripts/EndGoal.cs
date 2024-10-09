using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGoal : MonoBehaviour
{
	public delegate void Simple();
	public event Simple EndGoalReached_Event;
	
	private void OnTriggerEnter(Collider other)
	{
		EndGoalReached_Event?.Invoke();
	}
}
