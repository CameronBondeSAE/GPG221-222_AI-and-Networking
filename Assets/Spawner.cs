using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private GameObject prefab;

	public int amount = 10;
	
	// Start is called before the first frame update
    void Start()
    {
	    for (int i = 0; i < amount; i++)
	    {
		    Instantiate(prefab, transform.position, Quaternion.identity);
	    }
    }
}
