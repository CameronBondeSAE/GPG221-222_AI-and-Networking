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