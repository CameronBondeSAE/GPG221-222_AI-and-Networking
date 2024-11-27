using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ChestType
{
	Closed,
	Open,
	Destroyed
}

public class Chest : NetworkBehaviour
{
	public NetworkVariable<ChestType> currentState;
	public NetworkList<bool>          states;

	public Transform lid;

	private void Awake()
	{
		states = new NetworkList<bool>();
		states.Add(false);
		states.Add(false);
		states.Add(false);
		states.Add(false);
		states.Add(false);
		states.Add(false);
	}

	void OnEnable()
	{
		currentState.OnValueChanged += OnStateChanged_View;
		
		states.OnListChanged += StatesOnOnListChanged_Client;
	}

	// Client
	private void StatesOnOnListChanged_Client(NetworkListEvent<bool> changeevent)
	{
		Debug.Log(changeevent.Value);
		DebugAllStates();
	}

	void OnDisable()
	{
		currentState.OnValueChanged -= OnStateChanged_View;
	}

	/// <summary>
	/// View only
	/// </summary>
	/// <param name="previousValue"></param>
	/// <param name="newValue"></param>
	private void OnStateChanged_View(ChestType previousValue, ChestType newValue)
	{
		if (newValue == ChestType.Open)
		{
			Open();
		}
		if (newValue == ChestType.Closed)
		{
			Close();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (currentState.Value == ChestType.Open)
				currentState.Value = ChestType.Closed;
			if (currentState.Value == ChestType.Closed)
				currentState.Value = ChestType.Open;
			
			if(states.Count > 0)
				states[Random.Range(0, states.Count)] = true;

			DebugAllStates();
		}
	}

	private void DebugAllStates()
	{
		Debug.Log("All networklist bools");
		foreach (bool state in states)
		{
			Debug.Log(" - "+state);
		}
	}

	// Update is called once per frame
	[Button]
	void Thing()
	{
		Debug.Log("I am Chest");
	}

	[Button]
	public void Open()
	{
		lid.transform.DOLocalRotate(new Vector3(110f, 0, 0f), 13f).SetEase(Ease.InOutQuad);
	}
	public void Close()
	{
		lid.transform.DOLocalRotate(new Vector3(0, 0, 0f), 11f).SetEase(Ease.InExpo);
	}
	
	
	public Transform       target;
	public List<Transform> inZone;
	
	private void OnTriggerEnter(Collider other)
	{
		inZone.Add(other.transform);
	}

	private void OnTriggerExit(Collider other)
	{
		inZone.Remove(other.transform);
	}
}