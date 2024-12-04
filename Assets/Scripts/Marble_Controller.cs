using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Marble_Controller : MonoBehaviour
{
	public Marble_Model marbleModel;
	public KeyCode      jump = KeyCode.Space;

	// Update is called once per frame
	void Update()
	{
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		marbleModel.Roll(direction);

		if (Input.GetKeyDown(jump))
		{
			Debug.Log("Controller Jump");
			marbleModel.Jump_Rpc();
		}
	}
}