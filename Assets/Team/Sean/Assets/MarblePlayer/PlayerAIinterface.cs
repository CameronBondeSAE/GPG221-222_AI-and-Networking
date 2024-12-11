using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIinterface : MonoBehaviour
{
    MarbleController control;
//Vector3 input;
    Vector3 movementDir;
    public int id;
    // Start is called before the first frame update
    void Awake()
    {
        control = GetComponent<MarbleController>();
        RandomID();
    }

    // Update is called once per frame
    void Update()
    {
        movementDir = control.moveDir;
    }

    void RandomID()
    {
        id = Random.Range(0, 100);
    }
}
