using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public enum States
	{
		Attacking,
		RunningAway,
		Idle,
		Patrolling
	}
	
	
	public States currentState;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(currentState == States.Attacking)
	    {
		    // Attack
		    // audioSource.Play(monsterYell);
	    }
	    if(currentState == States.Idle)
	    {
		    // Idle
	    }
	    if(currentState == States.Patrolling)
	    {
	    }
	    if(currentState == States.RunningAway)
	    {
	    }
    }
}
