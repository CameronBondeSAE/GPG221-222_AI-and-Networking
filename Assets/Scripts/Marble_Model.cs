using Unity.Netcode;
using UnityEngine;

[SelectionBase]
public class Marble_Model : NetworkBehaviour
{
	// Variables
	// They store information
	// This one stores which 'Rigidbody' component we want to talk to. They do the physics movement
	public Rigidbody rb;
	// This one stores a 'float' which is just a number. You can change these in the editor
	[Tooltip("The is the rotation speed")]
	public float speed = 25f;
	
	public void Roll(Vector3 direction)
	{
		// We're talking to the Rigidbody via our variable. Note the dot. This will show you everything that component can do
		rb.AddTorque(direction * speed);
	}

	[Rpc(SendTo.ClientsAndHost, RequireOwnership = true)]
	public void Jump_Rpc()
	{
		Debug.Log("Actual Jump");
		rb.AddForce(Vector3.up * speed, ForceMode.VelocityChange);
	}
}