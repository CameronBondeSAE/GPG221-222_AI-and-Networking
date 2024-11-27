using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsVisible : MonoBehaviour
{
	public bool          isVisible;
	public MonoBehaviour target;
	public Rigidbody     rb;
	
	// WARNING: Also works when the editor camera sees it!!!
    void OnBecameVisible()
    {
	    // if (Camera.current == Camera.main)
	    {
		    // Debug.Log("Visible");
		    // target.enabled = false;
		    rb.isKinematic = true;
		    isVisible      = true;
	    }
    }

    private void OnBecameInvisible()
    {
	    // if (Camera.current == Camera.main)
	    {
		    // target.enabled = true;
		    rb.isKinematic = false;
		    // Debug.Log("Not visible");
		    isVisible = false;
	    }
    }
}
