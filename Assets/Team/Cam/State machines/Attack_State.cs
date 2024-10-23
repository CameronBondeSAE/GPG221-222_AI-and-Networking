using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_State : MonoBehaviour
{
	public Transform t;
	
	private void OnEnable()
	{
		Debug.Log("Attack enter");
	}

	private void Update()
	{
		t.Rotate(Time.deltaTime*10f,Time.deltaTime*100f,Time.deltaTime*30f);
	}

	private void OnDisable()
	{
		Debug.Log("Attack exit");
		t.rotation = Quaternion.identity;
	}
}
