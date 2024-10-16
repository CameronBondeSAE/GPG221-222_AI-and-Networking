using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class NeighbourManager_EB : MonoBehaviour
    {
        public List<Transform> neighbours = new List<Transform>();

        public LayerMask Critters;

        public void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger && other.CompareTag("Critters"))
            {
                neighbours.Add(other.transform);
            }

        }

        public void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger && other.CompareTag("Critters"))
            {
                neighbours.Remove(other.transform);
            }

        }
    }
}

