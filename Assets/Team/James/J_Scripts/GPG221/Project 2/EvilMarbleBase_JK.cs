using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

public class EvilMarbleBase_JK : AntAIState
{
    public Transform EMHome;
    public Rigidbody EMrb;
    public float EMOrbitRadius = 5f;
    public float EMOrbitSpeed = 2f;
    public float angle = 0f;
    public float EMSpeed = 10f;
    public float EMChaseSpeed = 10f;
    public Renderer EMMarbleRenderer;
    public Vector3 chaseOffset;
    public float chaseDistance = 0f;
}
