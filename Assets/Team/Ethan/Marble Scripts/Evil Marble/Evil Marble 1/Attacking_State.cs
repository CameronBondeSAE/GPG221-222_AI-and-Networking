using EB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attacking_State : EvilMarbleBase
{
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public SearchPlayerClose searchPlayerClose;
    public TextFaceCameraEvilMarble textEvilMarble;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
    }

    public override void Enter()
    {
        base.Enter();
        EvilMarbleBase.marbleRenderer.material.color = Color.red;

        textEvilMarble.GetComponent<TMP_Text>().text = "Attack State";
    }
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        EvilRB.position = Vector3.MoveTowards(EvilRB.position, searchPlayerClose.playerTransform.position, Time.deltaTime * EvilMarbleBase.chaseSpeed);
    }
}
