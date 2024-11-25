using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIinterface : MonoBehaviour
{
    MarbleController control;
//Vector3 input;
    Vector3 movementDir;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<MarbleController>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDir = control.moveDir;
    }
}
