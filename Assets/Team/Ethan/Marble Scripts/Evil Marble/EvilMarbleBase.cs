using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMarbleBase : AntAIState
{
    public Transform home;
    public Rigidbody rb;
    public float orbitSpeed = 2f;
    public float orbitRadius = 5f;
    public float angle = 0f;
    public float speed = 10f;
    public float chaseSpeed = 10f;
    public Renderer marbleRenderer;
    [SerializeField]  public List<Transform> blockLocations = new List<Transform>();
    public Transform currentBlockTarget;
    public float targetReachThreshold = 0.5f;
    public float movementSpeed = 5f;
}
