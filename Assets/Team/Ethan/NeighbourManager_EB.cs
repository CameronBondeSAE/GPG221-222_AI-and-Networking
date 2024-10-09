using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourManager_EB : MonoBehaviour
{
    public List<Transform> neighbours = new List<Transform>();

    public void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            neighbours.Add(other.transform);
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            neighbours.Remove(other.transform);
        }
        
    }
}
