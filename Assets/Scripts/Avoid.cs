using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
	public Rigidbody rb;
	[SerializeField]
	private float turnSpeed = 10f;

	[SerializeField]
	private float distance = 2.5f;

	void FixedUpdate()
    {
	    bool hitSomething = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance);

	    if (hitSomething)
	    {
		    rb.AddRelativeTorque(0, turnSpeed, 0);
	    }
    }
}
