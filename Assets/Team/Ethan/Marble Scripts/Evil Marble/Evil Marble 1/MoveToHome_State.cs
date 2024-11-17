using EB;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class MoveToHome_State : EvilMarbleBase
{
    //References to other gameobjects/scripts
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public TextFaceCameraEvilMarble textEvilMarble;
    public EvilMarbleSensor evilMarbleSensor;

    //make sure when created to get all references
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
        evilMarbleSensor = aGameObject.GetComponent<EvilMarbleSensor>();
    }

    //when entering the state change colour and txt
    public override void Enter()
    {
        base.Enter();

        EvilMarbleBase.marbleRenderer.material.color = Color.blue;

        textEvilMarble.GetComponent<TMP_Text>().text = "Home State";

    }

    //Get position of home and move to home
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Vector3 targetDir;
        targetDir = (EvilMarbleBase.home.position - transform.position).normalized;

        EvilRB.AddForce(targetDir);
    }
}
