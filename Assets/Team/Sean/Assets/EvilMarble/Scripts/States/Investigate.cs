using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Investigate : PredictorBase
{
    float timer;
    DetectionMethods detectionMethods;
    AIMarbleTarget targetscript;
    Vector3 pingloc;
    
    //Set target to sound point

    //Stay in area for a small time

    //If it sees player then chase

    //If it doesn't see player then reset

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    //On creation
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        detectionMethods = aGameObject.GetComponent<DetectionMethods>();
        targetscript = aGameObject.GetComponent<AIMarbleTarget>();
        




    }



    //On entering state
    public override void Enter()
    {
        
        base.Enter();
        timer = detectionMethods.investigationTime;
        if (FindObjectOfType<noiseping>() != null)
        {
            detectionMethods.statusText.text = "Investigating noise";
            pingloc = GameObject.FindGameObjectWithTag("Noise").transform.position;

        }
    }

    //Calls when state is active
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        //Debug.Log("Investigating area");
        timer -= aDeltaTime;
        
            targetscript.agentTarget.transform.position = pingloc;
        
        if (timer < 0)
        {
            detectionMethods.Reset = true;
            detectionMethods.NoiseDetected = false;
        }
    }
}
