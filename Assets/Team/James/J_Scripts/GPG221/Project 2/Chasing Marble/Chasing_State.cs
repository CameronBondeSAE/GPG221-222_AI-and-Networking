using EB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chasing_State : EvilMarbleBase_JK
{
    public GameObject evilMarble;
    public Rigidbody evilMarbleRB;
    public EvilMarbleBase_JK evilMarbleBase_JK;
    public ChasingEvilMarbleDetection chasingEvilMarbleDetection;
    public TextFaceCameraEvilMarble textEvilMarble;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        evilMarbleRB = aGameObject.GetComponent<Rigidbody>();
        evilMarbleBase_JK = aGameObject.GetComponent<EvilMarbleBase_JK>();
        chasingEvilMarbleDetection = aGameObject.GetComponent<ChasingEvilMarbleDetection>();
        textEvilMarble = aGameObject.GetComponentInChildren<TextFaceCameraEvilMarble>();
    }

    public override void Enter()
    {
        base.Enter();
        evilMarbleBase_JK.EMMarbleRenderer.material.color = Color.blue;
        textEvilMarble.GetComponent<TMP_Text>().text = "Chase State";
    }
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        evilMarbleBase_JK.chaseDistance += evilMarbleBase_JK.EMOrbitSpeed * Time.deltaTime;

        float x = evilMarbleBase_JK.EMHome.position.x + Mathf.Cos(evilMarbleBase_JK.chaseDistance) * evilMarbleBase_JK.EMOrbitRadius;
        float z = evilMarbleBase_JK.EMHome.position.z + Mathf.Sin(evilMarbleBase_JK.chaseDistance) * evilMarbleBase_JK.EMOrbitRadius;

        Vector3 newCirclePosition = new Vector3(x, evilMarbleRB.position.y, z);   

        evilMarbleRB.position = Vector3.MoveTowards(evilMarbleRB.position, newCirclePosition, Time.deltaTime * evilMarbleBase_JK.EMChaseSpeed);

       
    }
}
