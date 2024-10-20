using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class SpawnerPath : MonoBehaviour
    {
        //[SerializeField] private GameObject prefab;
        //public int amount = 1;

        //public void SpawnMany()
        //{
        //    for (int i = 0; i < amount; i++)
        //    {
        //        Spawn();
        //    }
        //}

        //public void Spawn()
        //{
        //    GameObject newAI = Instantiate(prefab, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));

        //    MoveTarget moveTarget = newAI.GetComponent<MoveTarget>();
        //    TurnTowards_EB turnTowardsScript = newAI.GetComponent<TurnTowards_EB>();
        //    NavMeshPath_EB navMeshPath = newAI.GetComponent<NavMeshPath_EB>();

        //    if (moveTarget != null && turnTowardsScript != null && navMeshPath != null)
        //    {
        //        navMeshPath.targetPoint = moveTarget.transform; // Set target point for NavMesh
        //        navMeshPath.FindPath(); // Calculate the path to the target

        //        turnTowardsScript.ResetPath(); // Ensure TurnTowards starts at the first corner
        //        turnTowardsScript.SetTargetObject(moveTarget.transform); // Set the target object for TurnTowards
        //    }
        //    else
        //    {
        //        Debug.LogError("One of the required components (MoveTarget, TurnTowards_EB, NavMeshPath_EB) is missing on the spawned AI.");
        //    }
        //}
    }
}
