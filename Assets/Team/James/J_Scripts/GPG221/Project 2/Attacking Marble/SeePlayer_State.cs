using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamesKilpatrick;
using EB;
using TMPro;

public class SeePlayer_State : EvilMarbleBase_JK
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
        evilMarbleBase_JK.EMMarbleRenderer.material.color = Color.magenta;
        textEvilMarble.GetComponent<TMP_Text>().text = "Search State";

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        evilMarbleBase_JK.angle += evilMarbleBase_JK.EMOrbitSpeed * Time.deltaTime;

        float x = evilMarbleBase_JK.EMHome.position.x + Mathf.Cos(evilMarbleBase_JK.angle) * evilMarbleBase_JK.EMOrbitRadius;
        float z = evilMarbleBase_JK.EMHome.position.z + Mathf.Sin(evilMarbleBase_JK.angle) * evilMarbleBase_JK.EMOrbitRadius;

        Vector3 newCirclePosition = new Vector3(x, evilMarbleRB.position.y, z);

        Debug.DrawRay(newCirclePosition, Vector3.up * 10);

        Vector3 targetDirection;
        targetDirection = (newCirclePosition - transform.position).normalized;

        evilMarbleRB.AddForce(targetDirection * evilMarbleBase_JK.EMSpeed);
    }

}

