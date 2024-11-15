using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTargetClose_State : EvilMarbleBase
{
    public GameObject evilMarble;
    public Rigidbody EvilRB;
    public EvilMarbleBase EvilMarbleBase;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        evilMarble = aGameObject;
        EvilRB = aGameObject.GetComponent<Rigidbody>();
        EvilMarbleBase = aGameObject.GetComponent<EvilMarbleBase>();
    }

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
