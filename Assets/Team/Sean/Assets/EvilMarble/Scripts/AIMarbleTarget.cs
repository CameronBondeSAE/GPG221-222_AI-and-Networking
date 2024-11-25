using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMarbleTarget : MonoBehaviour
{
    Rigidbody rb;

     public GameObject agentTarget;
    DetectionMethods detectionMethods;
    public Vector3 targetOffset = new Vector3 (0,0,0);
    public float ResetTimer = 8f;
    float timer;
    public float moveForce = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        detectionMethods = GetComponent<DetectionMethods>();
    }

    void Update()
    {
        MoveAgent();

        timer -= Time.deltaTime;

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerAIinterface>() != null && timer < 0)
        {
            detectionMethods.PlayerDetected = true;
            Debug.Log("Player found");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerAIinterface>() != null && timer < 0)
        {
            detectionMethods.Chasingplayer = true;
            timer = ResetTimer;
            Debug.Log("Player Hit");
        }
    }



    Vector3 CalculateDistanceToTarget()
    {
        return (agentTarget.transform.position - transform.position);
    }

    void MoveAgent()
    {
        //minus the Y direction to make force flat on 2d plane
        rb.AddForce( CalculateForce() );
    }


    //Anti force to slow down nearing target
    Vector3 CalculateForce()
    {

        Vector3 movementDirection = ((CalculateDistanceToTarget().normalized) - new Vector3(0, CalculateDistanceToTarget().normalized.y,0));


        Vector3 calculateForce = movementDirection * moveForce;
        Vector3 counterForce = -movementDirection * moveForce;
        return calculateForce ;
        //return CalculateDistanceToTarget().normalized * ((CalculateDistanceToTarget().magnitude * 2)/(-rb.velocity.magnitude * -rb.velocity.magnitude) * 0.1f);
    }

    /* 
     * F = ma (m = 1 so F = a)
     * 
     * v^2 = u^2 + 2as
     * v = 0
     * u = rb.velocity.magnitude
     * (-u^2)/2s = a
     * 
     * (-rb.velocity.magnitude^2)/(CalculateDistanceToTarget().magnitude * 2 = a
     * */
    // Update is called once per frame
    
}
