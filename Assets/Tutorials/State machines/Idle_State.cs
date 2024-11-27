using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : MonoBehaviour
{
	public Transform t;
	
	private void OnEnable()
	{
		Debug.Log("Idle enter");
	}

	private void Update()
	{
		t.Rotate(0,Time.deltaTime*8f,0);
	}

	private void OnDisable()
	{
		Debug.Log("Idle exit");
	}
}
