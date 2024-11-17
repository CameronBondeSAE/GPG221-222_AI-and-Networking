using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JamesKilpatrick
{
    public class Pathfinder : MonoBehaviour
    {
        // NAVMESH
        // Variable that gets filled in with the points
        public NavMeshPath path;
        public Transform target;

        public Vector3 lastPoint;
        public Vector3[] pathCorners;

        public int cornerIndex = 0;
        private float minimumDistanceToCorner = 3f;


        // ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
        void Start()
        {
            CreatePath();
        }
        private void Update()
        {
            foreach (Vector3 point in path.corners)
            {
                Debug.DrawLine(lastPoint, point, Color.green);
                lastPoint = point;
            }


            if (cornerIndex < path.corners.Length)
            {
                // See if we are close to the corner
                if (Vector3.Distance(transform.position, path.corners[cornerIndex]) < minimumDistanceToCorner)
                {
                    cornerIndex++;
                    if (cornerIndex < path.corners.Length)
                    {
                        GetComponent<TurnTowards>().targetPosition = path.corners[cornerIndex];
                    }                
                }
            }
            if (cornerIndex >= path.corners.Length)
            {
                GetComponent<TurnTowards>().targetPosition = target.position;
            }


        }

        public void CreatePath()
        {
            lastPoint = transform.position;

            // Create it in Awake or something
            path = new NavMeshPath();


            // Call this when you want to go somewhere! Then read the path variable and you’ll see
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

            cornerIndex = 0;

            Debug.Log("Created new path to target");

        }
    }
}
