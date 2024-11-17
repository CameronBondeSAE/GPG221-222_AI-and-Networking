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
        public Vector3 newPosition;
        
        private void FixedUpdate()
        {
           
            if (pathfinder.path != null)
            {
                if (cornerIndex < pathfinder.path.corners.Length)
                {
                    if (Vector3.Distance(transform.position, pathfinder.path.corners[cornerIndex]) < minimumDistanceToCorner)
                    {
                        cornerIndex++;
                        turnTowards.targetPosition = pathfinder.path.corners[cornerIndex];
                        /*if (cornerIndex > pathfinder.path.corners.Length)
                        {
                           turnTowards.targetPosition = new Vector3(0,0,0); 
                        }*/
                    }
                   
                }
               /* if (cornerIndex > pathfinder.path.corners.Length)
                {
                    turnTowards.targetPosition = new Vector3(0, 0, 0);
                } */
                else
                {
                    enabled = false;
                }

            }
            
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (pathfinder.path != null)
                if (pathfinder.path.corners.Length > 0)
                {
                    Gizmos.DrawSphere(pathfinder.path.corners[cornerIndex], minimumDistanceToCorner / 3f);
                }
        }

        private void ChangePosition()
        {
            
        }

    }
}
