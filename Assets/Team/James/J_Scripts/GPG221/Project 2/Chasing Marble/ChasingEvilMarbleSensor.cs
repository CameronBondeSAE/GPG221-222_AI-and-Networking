using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JamesKilpatrick
{
    public enum ChasingEvilMarble_JK
    {
        IsHome = 0,
        SeesPlayer = 1,
        IsChasing = 2
    }
    public class ChasingEvilMarbleSensor : EvilMarbleBase_JK, ISense
    {
        //Bools are used to allow easy acces in inspector or scripts
        public bool IsHome = false;
        public bool IsChasing = false;
        public bool SeesPlayer = false;

        //Need to set world state for AI planner
        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.Set(ChasingEvilMarble_JK.IsChasing, IsChasing);
            aWorldState.Set(ChasingEvilMarble_JK.IsHome, IsHome);
            aWorldState.Set(ChasingEvilMarble_JK.SeesPlayer, SeesPlayer);
        }


    }
}
