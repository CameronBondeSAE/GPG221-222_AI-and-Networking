using Anthill.AI;
using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public enum EvilMarble_EB2
    {
	   IsHome = 0,
	   IsBlocking = 1,
	   SeeTargetClose = 2
    }
    public class EvilMarbleSensorss : EvilMarbleBase, ISense
    {
        //Bools to easily access from inspector or scripts
        public bool IsBlocking = false;
        public bool IsHome = false;
        public bool SeeTargetClose = false;

        //Sets world state for Planner
        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.Set(EvilMarble_EB2.IsBlocking, IsBlocking);
            aWorldState.Set(EvilMarble_EB2.IsHome, IsHome);
            aWorldState.Set(EvilMarble_EB2.SeeTargetClose, SeeTargetClose);
        }
    }
}

