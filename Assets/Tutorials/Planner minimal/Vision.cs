using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	public Panic_Sensor panicSensor;
	// List of everything I currently can sense in my trigger, not just one thing
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			panicSensor.seePlayer = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			panicSensor.seePlayer = false;
		}
	}
}
