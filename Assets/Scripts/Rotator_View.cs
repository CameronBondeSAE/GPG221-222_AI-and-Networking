using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator_View : MonoBehaviour
{
	public Rotator Rotator;
	public MeshRenderer MeshRenderer;
	
    // Update is called once per frame
    void Update()
    {
	    if (Rotator.state == Rotator.States.Idle)
	    {
		    MeshRenderer.material.color = Color.blue;
	    }

	    if (Rotator.state == Rotator.States.Rotating)
	    {
		    MeshRenderer.material.color = Color.green;
	    }
    }
}
