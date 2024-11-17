using EB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchTargetClose_State : EvilMarbleBase
{
    //References to other gameobjects/scripts
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;
    public TextFaceCameraEvilMarble textEvilMarble;

    //make sure when created to get all references
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
    }

    //when entering the state change colour and txt
    public override void Enter()
    {
        base.Enter();
        EvilMarbleBase.marbleRenderer.material.color = Color.green;

        textEvilMarble.GetComponent<TMP_Text>().text = "Search State";
    }

    //Patrol home by following a circle pattern
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        EvilMarbleBase.angle += EvilMarbleBase.orbitSpeed * Time.deltaTime;

        float x = EvilMarbleBase.home.position.x + Mathf.Cos(EvilMarbleBase.angle) * EvilMarbleBase.orbitRadius;
        float z = EvilMarbleBase.home.position.z + Mathf.Sin(EvilMarbleBase.angle) * EvilMarbleBase.orbitRadius;

        Vector3 newCirclePosition = new Vector3(x, EvilRB.position.y, z);

        Debug.DrawRay(newCirclePosition, Vector3.up *10);

        Vector3 targetDir;
        targetDir = (newCirclePosition - transform.position).normalized;

        EvilRB.AddForce(targetDir * EvilMarbleBase.speed);
    }
}
