using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamesKilpatrick;
using EB;
using TMPro;

public class Attack_JK_State : EvilMarbleBase_JK
{
    public GameObject evilMarble;
    public Rigidbody evilMarbleRB;
    public EvilMarbleBase_JK evilMarbleBase_JK;
    public EvilMarbleDetection evilMarbleDetection;
    public TextFaceCameraEvilMarble textEvilMarble;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        evilMarbleRB = aGameObject.GetComponent<Rigidbody>();
        evilMarbleBase_JK = aGameObject.GetComponent<EvilMarbleBase_JK>();
        evilMarbleDetection = aGameObject.GetComponent<EvilMarbleDetection>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
    }

    public override void Enter()
    {
        base.Enter();
        evilMarbleBase_JK.EMMarbleRenderer.material.color = Color.red;
        textEvilMarble.GetComponent<TMP_Text>().text = "Attack State";

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        evilMarbleRB.position = Vector3.MoveTowards(evilMarbleRB.position, evilMarbleDetection.playerTransform.position, Time.deltaTime * evilMarbleBase_JK.EMChaseSpeed);
    }
}

