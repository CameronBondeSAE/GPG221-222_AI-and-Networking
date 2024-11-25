using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Patrol : PredictorBase
{
    // Start is called before the first frame update
    DetectionMethods detectionMethods;
    AIMarbleTarget targetscript;
    float timer;
    int patrolindex = 0;
    


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
        Debug.Log("Patrolling");
        timer = detectionMethods.patroltimer;
        targetscript.agentTarget.transform.position = detectionMethods.patrolAreas[patrolindex].position;
        detectionMethods.statusText.text = "Patrolling";
    }

    



    //Calls when state is active
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        timer -= aDeltaTime;
        
        if (timer < 0)
        {
            
            patrolindex += 1;
            timer = detectionMethods.patroltimer;
            Debug.Log(patrolindex);
            if (patrolindex > detectionMethods.patrolAreas.Length - 1)
            {
                patrolindex = 0;
            }
            targetscript.agentTarget.transform.position = detectionMethods.patrolAreas[patrolindex].position;
        }

        if (detectionMethods.canlisten)
        {
            if (GameObject.FindGameObjectWithTag("Noise") != null)
            {
                detectionMethods.NoiseDetected = true;
            }
        }
    }
}
