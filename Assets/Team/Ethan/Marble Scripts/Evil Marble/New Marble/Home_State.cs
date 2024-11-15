using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Home_State : EvilMarbleBase
{

    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("I Am Home");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        

        Vector3 targetDir;
        targetDir = (EvilMarbleBase.home.position - transform.position).normalized;

        EvilRB.AddForce(targetDir);
    }
}
