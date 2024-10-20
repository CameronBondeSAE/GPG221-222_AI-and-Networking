using JamesKilpatrick;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


namespace EB
{
    public class TurnTowards_EB : MonoBehaviour
    {
        public float turnSpeed = 2;
        public float movementSpeed = 5; 
        public float minimumDistanceToCorner = 2f;

        public NavMeshPath_EB nav;
        public int cornerIndex = 0;

        void Start()
        {
            nav = GetComponent<NavMeshPath_EB>();
        }

        void FixedUpdate()
        {
            if (nav == null || nav.path == null || nav.path.corners.Length == 0)
            {
                return; 
            }

            if (cornerIndex >= nav.path.corners.Length)
            {
                cornerIndex = nav.path.corners.Length - 1; 
                return; 
            }

            Vector3 targetCorner = nav.path.corners[cornerIndex];
            Vector3 targetDir = (targetCorner - transform.position).normalized;

            float step = turnSpeed * Time.fixedDeltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            
            if (Vector3.Distance(transform.position, targetCorner) > minimumDistanceToCorner)
            {
                
                transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
            }
            else
            {
                cornerIndex++;

                if (cornerIndex >= nav.path.corners.Length)
                {
                    cornerIndex = nav.path.corners.Length - 1; 
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (nav != null && nav.path != null && cornerIndex < nav.path.corners.Length)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(nav.path.corners[cornerIndex], minimumDistanceToCorner / 2f);
            }
        }
    }

}



