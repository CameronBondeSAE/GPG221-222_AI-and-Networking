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
        public Transform targetPoint;

        public Vector3 lastPoint;

        public void Start()
        {
           
        }

        // ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
        void Update()
        {
            lastPoint = transform.position;

            // Create it in Awake or something
            path = new NavMeshPath();


            // Call this when you want to go somewhere! Then read the path variable and you’ll see
            NavMesh.CalculatePath(transform.position, targetPoint.position, NavMesh.AllAreas, path);

           foreach (Vector3 point in path.corners) 
            {             
                Debug.DrawLine(lastPoint, point, Color.green);
                lastPoint = point;
            }
        }
    }
}
