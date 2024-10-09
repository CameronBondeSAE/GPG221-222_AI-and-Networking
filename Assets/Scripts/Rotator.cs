using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Rotator : NetworkBehaviour
{
	// Variables
	// This links your code to any other code in the whole game. 
	// In this case it's like a wire that can hook in to a "Transform" component script
	public Transform myTransform;
	public float speed = 1f;
	
	public enum States
	{
		Idle,
		Rotating
	}

	public States state;


	private void Start()
	{
		// if (IsServer)
		{
			StartCoroutine(RotateSequence());
		}
	}

	private IEnumerator RotateSequence()
	{
		while (true)
		{
			state = States.Idle;
			yield return new WaitForSeconds(1f);
			state = States.Rotating;
			yield return new WaitForSeconds(2f);
		}
	}

	// Functions
	// This one gets called by Unity itself, 60 times a second
	void FixedUpdate()
	{
		//if (IsServer)
		{
			if (state == States.Idle)
			{
				// Do idle
			}

			if (state == States.Rotating)
			{
				// Do rotate
				// Now we've wired it up, we can tell it to do.. whatever it can do! Note the dot
				myTransform.Rotate(0, speed, 0);
			}
		}
	}
}