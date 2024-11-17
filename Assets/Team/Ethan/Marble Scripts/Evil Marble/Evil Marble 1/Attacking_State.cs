using EB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attacking_State : EvilMarbleBase
{
    //References to other gameobjects/scripts
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public SearchPlayerClose searchPlayerClose;
    public TextFaceCameraEvilMarble textEvilMarble;

    //make sure when created to get all references
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
    }

    //when entering the state change colour and txt
    public override void Enter()
    {
        base.Enter();
        EvilMarbleBase.marbleRenderer.material.color = Color.red;

        textEvilMarble.GetComponent<TMP_Text>().text = "Attack State";
    }

    //Chase player 
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        EvilRB.position = Vector3.MoveTowards(EvilRB.position, searchPlayerClose.playerTransform.position, Time.deltaTime * EvilMarbleBase.chaseSpeed);
    }
}
