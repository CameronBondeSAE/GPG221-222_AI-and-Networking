using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTarget()
    {
        target.transform.position = new Vector3(Random.Range(44, 95), 1, Random.Range(47, 112));
        Debug.Log("Target moved");
    }

}
