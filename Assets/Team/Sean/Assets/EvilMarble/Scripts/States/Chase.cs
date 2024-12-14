using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chase : PredictorBase
{
    // Start is called before the first frame update
    DetectionMethods detectionMethods;
    AIMarbleTarget targetscript;
    PlayerAIinterface player;
    
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
        player = FindObjectOfType<PlayerAIinterface>();
        


    }

    //On entering state
    public override void Enter()
    {
        
        base.Enter();
        //Debug.Log("Chasing player");
        //detectionMethods.Chasingplayer = true;
        detectionMethods.statusText.text = "Chasing player";

    }




    //Calls when state is active
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if(!targetscript.directChase){
        targetscript.agentTarget.transform.position = player.transform.position + player.gameObject.GetComponent<Rigidbody>().velocity;
        }
        else
        {
            targetscript.agentTarget.transform.position = player.transform.position;
        }
        

    }
}
