using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CameronBonde
{
	public class PathfindNavmesh : MonoBehaviour
	{
		// NAVMESH
		// Variable that gets filled in with the points
		public NavMeshPath path;
		public Transform targetPoint;

		public TurnTowards turnTowards;
		
		public int cornerIndex = 0;
		[SerializeField]
		private float minimumDistanceToCorner = 2f;


		// ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
		void Update()
		{
			// Create it in Awake or something
			path = new NavMeshPath();


			// Call this when you want to go somewhere! Then read the path variable and you’ll see
			NavMesh.CalculatePath(transform.position, targetPoint.position, NavMesh.AllAreas, path);

			Vector3 lastPoint = transform.position;
			
			foreach (Vector3 pathCorner in path.corners)
			{
				Debug.DrawLine(lastPoint, pathCorner, Color.red);
				lastPoint = pathCorner;
			}
			
			
			// See if we are close to the corner
			if (Vector3.Distance(transform.position, path.corners[cornerIndex]) < minimumDistanceToCorner)
			{
				cornerIndex++;
				GetComponent<TurnTowards>().targetPosition = path.corners[cornerIndex];
			}

		}
		
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			if (path != null)
				if (path.corners.Length > 0)
					Gizmos.DrawSphere(path.corners[cornerIndex], minimumDistanceToCorner / 2f);
		}
	}
}