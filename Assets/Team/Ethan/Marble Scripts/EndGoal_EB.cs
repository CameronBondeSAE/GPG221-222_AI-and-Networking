using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal_EB : MonoBehaviour
{

    public delegate void Simple();


    public event Simple EndGoalReached_Event;


    private void OnTriggerEnter(Collider other)
    {
        EndGoalReached_Event?.Invoke();
    }

}
