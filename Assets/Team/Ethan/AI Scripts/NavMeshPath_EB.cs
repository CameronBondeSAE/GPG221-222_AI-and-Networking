using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EB
{
    public class NavMeshPath_EB : MonoBehaviour
    {
        public NavMeshPath path;
        public List<Transform> targetPoint = new List<Transform>();
        public int index = 0;
        public float reachThreshold = 0.5f;

        private Vector3 lastTargetPosition;

        void Start()
        {
            path = new NavMeshPath();
            lastTargetPosition = GetCurrentTargetPosition();
            CalculatePath();
        }

        private void Update()
        {
            if (targetPoint.Count == 0 || index >= targetPoint.Count)
            {
                Debug.LogWarning("Index is out of range.");
                return;
            }

            Vector3 currentTargetPosition = GetCurrentTargetPosition();
            if (currentTargetPosition != lastTargetPosition)
            {
                CalculatePath();
                lastTargetPosition = currentTargetPosition;
            }

            if (path.corners.Length > 1)
            {
                if (Vector3.Distance(transform.position, targetPoint[index].position) < reachThreshold)
                {
                    index++;
                    if (index >= targetPoint.Count)
                    {
                        index = 0; 
                    }
                    CalculatePath();
                }
            }
            else
            {
                Debug.LogWarning("No valid path corners");
            }
        }

        private void CalculatePath()
        {
            if (targetPoint.Count > index)
            {
                NavMesh.CalculatePath(transform.position, targetPoint[index].position, NavMesh.AllAreas, path);
            }
        }

        private Vector3 GetCurrentTargetPosition()
        {
            return targetPoint.Count > index ? targetPoint[index].position : Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            if (path != null && path.corners.Length > 1)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
                }
            }

            if (targetPoint != null)
            {
                Gizmos.color = Color.red;
                foreach (var target in targetPoint)
                {
                    if (target != null)
                    {
                        Gizmos.DrawSphere(target.position, 0.2f);
                    }
                }
            }
        }



    }
}

