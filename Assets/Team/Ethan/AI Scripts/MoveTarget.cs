using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EB
{
    public class MoveTarget : MonoBehaviour
    {

        public float xRange = 10f; 
        public float zRange = 10f; 
        public float yPosition = 0f; 

        public void MoveObjectRandomly()
        {
            float randomX = Random.Range(-xRange, xRange);
            float randomZ = Random.Range(-zRange, zRange);

            transform.position = new Vector3(randomX, yPosition, randomZ);
        }
    }
}

