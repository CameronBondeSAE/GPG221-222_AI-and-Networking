using System;
using UnityEngine;

namespace CameronBonde
{
	public class TurnTowards : MonoBehaviour
	{
		public float turnSpeed = 2;
		public Transform targetObject;
		public Vector3 targetPosition;
		public Rigidbody rb;


		//Update is called once per frame
		void FixedUpdate()
		{
			Vector3 targetDir;
			if (targetObject)
			{
				// Has a target gameobject
				targetDir = (targetObject.position - transform.position).normalized;
			}
			else
			{
				// Just a raw position in the world (for pathfinding points)
				targetDir = (targetPosition - transform.position).normalized;
			}

			float angle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);
			rb.AddRelativeTorque(0,angle*turnSpeed,0);
		}
	}
}