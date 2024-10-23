using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
	// Note the ‘virtual’ keywords. 
	// This allows this function to be ‘override’ overridden in other classes to customise what happens for each state

	public virtual void Enter()
	{
      	
	}

	public virtual void Execute()
	{
      
	}

	public virtual void Exit()
	{
      
	}
}