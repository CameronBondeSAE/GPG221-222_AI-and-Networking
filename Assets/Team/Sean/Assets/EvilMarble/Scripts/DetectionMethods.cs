using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using TMPro;

public enum MarbleStates
{
	PlayerDetected = 0,
    Reset = 1,
    Chasingplayer = 2,
    NoiseDetected = 3
}
public class DetectionMethods : MonoBehaviour, ISense
{
    
    //patrolling script section
    public Transform[] patrolAreas;
    public float patroltimer = 5f;
    public bool canlisten = false;
    public float investigationTime = 10f;
    AntAIAgent agent;
    public TextMeshPro statusText;

    
    
    

    public bool PlayerDetected = false;
    public bool Reset = true;
    public bool Chasingplayer = false;
    public bool NoiseDetected = false;



    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(MarbleStates.PlayerDetected, PlayerDetected);
        aWorldState.Set(MarbleStates.Reset, Reset);
        aWorldState.Set(MarbleStates.Chasingplayer, Chasingplayer);
        aWorldState.Set(MarbleStates.NoiseDetected, NoiseDetected);

        
    }

    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AntAIAgent>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
