using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_InheritanceState : StateBase
{
	public override void Enter()
	{
		base.Enter();
		
		Debug.Log("Idle enter");
	}

	public override void Execute()
	{
		base.Execute();
		
		
	}

	public override void Exit()
	{
		base.Exit();
		
		Debug.Log("Idle exit");
	}
}
