using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Alignment : MonoBehaviour
    {
       
        // Variable pointing to your Neighbours component
        public NeighboursManager neighbours;
        // Neighbours neighbours;
        public Rigidbody rb;


        public float force = 100f;

        void FixedUpdate()
        {
            
            // Some are Torque, some are Force		
           Vector3 targetDirection = CalculateMove(neighbours.neighboursList);

            // Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector. It CROSSES through both at 90 degrees
            Vector3 cross = Vector3.Cross(transform.forward, targetDirection);

            rb.AddTorque(cross * force);
            
        }

        public Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
                return Vector3.zero;

            Vector3 alignmentMove = Vector3.zero;

            // Average of all neighbours directions
            // I’m using a list of transforms in my neighbours script, you might be using GameObjects etc
            foreach (Transform item in neighbours)
            {
                alignmentMove += item.transform.forward;
            }

            alignmentMove /= neighbours.Count;

            return alignmentMove;
        }
    }
}
