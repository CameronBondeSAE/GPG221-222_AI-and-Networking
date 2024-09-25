using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ColourChanger : NetworkBehaviour
{
	[Header("Setup")]
	public Renderer rend;

	/// <summary>
	/// Server code
	/// Model (Pure game mechanics and rules)
	/// </summary>
	void FixedUpdate()
	{
		if (IsServer)
		{
			if (Random.Range(0, 100) == 0) // Server
			{
				Color colour = new Color(Random.value, Random.value, Random.value); // Server

				ChangeColour_Rpc(colour);
			}
		}
	}

	public void Update()
	{
		// Local only. Not networked
		if (IsLocalPlayer)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				GetBigOrDieTrying_RequestToServer_Rpc();
			}
		}
	}

	// Function that ONLY runs on the server. Typically for client controller code when they press buttons etc
	[Rpc(SendTo.Server, RequireOwnership = true, Delivery = RpcDelivery.Reliable)]
	private void GetBigOrDieTrying_RequestToServer_Rpc()
	{
		// Check if it's legal/not cheating
		GetBigOrDieTrying_ServerToClients_Rpc();
	}

	// Function that runs from the Server TO ALL clients
	[Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
	private void GetBigOrDieTrying_ServerToClients_Rpc()
	{
		// This is bugged
		transform.localScale = transform.localScale + new Vector3(1.25f, 1.25f, 1.25f);
	}

	/// <summary>
	/// Client side only
	/// View (Pure presentation to the player, graphics, sounds etc, no game logic)
	/// Remote Procedure Call
	/// </summary>
	/// <param name="colour">Colour for client. It's important for some reason</param>
	[Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
	public void ChangeColour_Rpc(Color colour)
	{
		// Debug.Log("HELLO: Server? = "+IsServer);
		// Debug.Log("HELLO: Client? = "+IsClient);
		rend.material.color = colour;
	}
}