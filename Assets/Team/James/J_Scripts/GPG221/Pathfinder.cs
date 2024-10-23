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


        // ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
        void Start()
        {
            
        }
        private void Update()
        {
            foreach (Vector3 point in path.corners)
            {
                Debug.DrawLine(lastPoint, point, Color.green);
                lastPoint = point;
            }
        }

        public void CreatePath()
        {
            lastPoint = transform.position;

            // Create it in Awake or something
            path = new NavMeshPath();


            // Call this when you want to go somewhere! Then read the path variable and you’ll see
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        }
    }
}
