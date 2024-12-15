using EB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchLocation_State1 : EvilMarbleBase
{
    //References to other gameobjects/scripts
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public TextFaceCameraEvilMarble textEvilMarble;
    public Vector3 targetLocation;
    public bool hasTargetLocation;
    public SearchPlayerClose searchPlayerClose;

    //make sure when created to get all references
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
    }

    //when entering the state change colour and txt
    public override void Enter()
    {
        base.Enter();
        EvilMarbleBase.marbleRenderer.material.color = Color.yellow;

        textEvilMarble.GetComponent<TMP_Text>().text = "Location State";
    }

    public void ReceiveLocation(Vector3 location)
    {
        targetLocation = location;
        hasTargetLocation = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        EvilRB.position = Vector3.MoveTowards(EvilRB.position, searchPlayerClose.soundLocation, Time.deltaTime * EvilMarbleBase.locationSpeed);
    }

}

