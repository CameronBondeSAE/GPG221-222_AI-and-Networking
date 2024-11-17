using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public enum EvilMarble_JK
    {
        IsHome = 0,
        SeesPlayer = 1,
        IsAttacking = 2
    }
    public class EvilMarbleSensor : EvilMarbleBase_JK, ISense
    {
        //Bools are used to allow easy acces in inspector or scripts
        public bool IsHome = false;
        public bool IsAttacking = false;
        public bool SeesPlayer = false;

        //Need to set world state for AI planner
        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.Set(EvilMarble_JK.IsAttacking, IsAttacking);
            aWorldState.Set(EvilMarble_JK.IsHome, IsHome);
            aWorldState.Set(EvilMarble_JK.SeesPlayer, SeesPlayer);
        }


    }
}
