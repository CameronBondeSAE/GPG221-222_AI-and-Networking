using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamesKilpatrick;
using EB;
using TMPro;

public class ReturnToHome_State : EvilMarbleBase_JK
{
    public GameObject evilMarble;
    public Rigidbody evilMarbleRB;
    public EvilMarbleBase_JK evilMarbleBase_JK;
    public TextFaceCameraEvilMarble textEvilMarble;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        evilMarbleRB = aGameObject.GetComponent<Rigidbody>();
        evilMarbleBase_JK = aGameObject.GetComponent<EvilMarbleBase_JK>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
    }

    public override void Enter()
    {
        base.Enter();
        textEvilMarble.GetComponent<TMP_Text>().text = "Home State";
        evilMarbleBase_JK.EMMarbleRenderer.material.color = Color.yellow;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Vector3 targetDirection;
        targetDirection = (evilMarbleBase_JK.EMHome.position - transform.position).normalized;
        evilMarbleRB.AddForce(targetDirection);
    }
}

