using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget_State : EvilMarbleBase
{
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public LayerMask detectionLayer;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Search Target Enter");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);


    }
}
