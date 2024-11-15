using Anthill.AI;
using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public enum EvilMarble_EB
    {
        IsHome = 0,
        IsAttacking = 1,
        SeeTargetClose = 2
    }
}
public class EvilMarbleSensor : EvilMarbleBase, ISense
{
    //Bools to easily access from inspector or scripts
    public bool IsAttacking = false;
    public bool IsHome = false;
    public bool IsBlocking = false;
    public bool SeeTargetFar = false;
    public bool SeeTargetClose = false;
    public bool PlayerDead = false;

    //Sets world state for Planner
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(EvilMarble_EB.IsAttacking, IsAttacking);
        aWorldState.Set(EvilMarble_EB.IsHome, IsHome);
        aWorldState.Set(EvilMarble_EB.SeeTargetClose, SeeTargetClose);
    }
}

