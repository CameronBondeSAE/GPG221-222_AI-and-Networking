using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace JamesKilpatrick
{
    public class PathFollow : MonoBehaviour
    {
        public Pathfinder pathfinder;
        public TurnTowards turnTowards;
        public int cornerIndex = 0;
        private float minimumDistanceToCorner = 3f;

        private void FixedUpdate()
        {
           
            if (pathfinder.path != null)
            {
                if (Vector3.Distance(transform.position, pathfinder.path.corners[cornerIndex]) < minimumDistanceToCorner)
                {
                    cornerIndex += 1;
                    turnTowards.targetPosition = pathfinder.path.corners[cornerIndex];
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (pathfinder.path != null)
                if (pathfinder.path.corners.Length > 0)
                    Gizmos.DrawSphere(pathfinder.path.corners[cornerIndex], minimumDistanceToCorner / 3f);
        }

    }
}
