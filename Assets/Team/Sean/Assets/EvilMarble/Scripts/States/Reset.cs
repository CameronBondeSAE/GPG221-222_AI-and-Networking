using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reset : PredictorBase
{
    DetectionMethods detectionMethods;
    AIMarbleTarget targetscript;
    
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
        
        detectionMethods.Reset = false;
        detectionMethods.PlayerDetected = false;
        detectionMethods.Chasingplayer = false;
        detectionMethods.NoiseDetected = false;
        detectionMethods.statusText.text = "Reset";
    }

    //Calls when state is active
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
    base.Execute(aDeltaTime, aTimeScale);
        //Debug.Log("Reset variables");
    }
    

}
