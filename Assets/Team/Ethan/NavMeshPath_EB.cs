using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPath_EB : MonoBehaviour
{
    // NAVMESH
    // Variable that gets filled in with the points
    public NavMeshPath path;
    public Transform targetPoint;

    public Vector3 firstPoint;
    public Vector3 lastPoint;


    // ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
    void Update()
    {
        // Create it in Awake or something
        path = new NavMeshPath();

        lastPoint = transform.position;

        // Call this when you want to go somewhere! Then read the path variable and you’ll see
        NavMesh.CalculatePath(transform.position, targetPoint.position, NavMesh.AllAreas, path);
        foreach (var corner in path.corners)
        {
            
            Debug.DrawLine(lastPoint, corner, Color.green);
            lastPoint = corner;
        }
        
    }

}
