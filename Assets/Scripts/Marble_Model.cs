using UnityEngine;

[SelectionBase]
public class Marble_Model : MonoBehaviour
{
	// Variables
	// They store information
	// This one stores which 'Rigidbody' component we want to talk to. They do the physics movement
	public Rigidbody rb;
	// This one stores a 'float' which is just a number. You can change these in the editor
	public float speed = 25f;
	
	public void Roll(Vector3 direction)
	{
		// We're talking to the Rigidbody via our variable. Note the dot. This will show you everything that component can do
		rb.AddTorque(direction * speed);
	}
}