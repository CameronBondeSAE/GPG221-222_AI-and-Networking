using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking_State : EvilMarbleBase
{
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public SearchPlayerClose searchPlayerClose;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        searchPlayerClose = aGameObject.GetComponent<SearchPlayerClose>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        EvilRB.position = Vector3.MoveTowards(EvilRB.position, searchPlayerClose.playerTransform.position, Time.deltaTime * EvilMarbleBase.chaseSpeed);
    }
}
