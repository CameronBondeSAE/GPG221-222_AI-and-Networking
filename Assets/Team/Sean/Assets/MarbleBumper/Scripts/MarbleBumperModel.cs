using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MarbleBumperModel : NetworkBehaviour
{
    public float cooldownTimer = 3f;
    public float bumperForce = 10f;
    private float cdTimer;
    private bool readyToFire = true;
    [SerializeField] MarbleBumperView mView;

    private bool validateCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        mView = gameObject.GetComponentInChildren<MarbleBumperView>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();

    }

    

    
    //Check object is in the collider
    private void OnTriggerStay(UnityEngine.Collider collision)
    {
        Trigger_RequestToServer_Rpc();
        if (readyToFire && validateCollision)
        {
            //If contains a rigidbody
            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                //calculate direction between bumper and collider
                 Vector3 direction = Vector3.Normalize(gameObject.transform.position - collision.transform.position)*-1;
                //Shoot an impulse force between bumper and the collision
                collision.GetComponent<Rigidbody>().AddForce(direction * bumperForce, ForceMode.Impulse);
                Debug.Log("Launched object");
                //Turn off ready to fire
                readyToFire = false;
                cdTimer = cooldownTimer;
                Debug.Log("On cooldown");
                mView.PlayAnimation();
            }
        }
    }

    

    [Rpc(SendTo.Server, RequireOwnership = false, Delivery = RpcDelivery.Reliable)]
    private void Trigger_RequestToServer_Rpc()
    {
        //made bool true on server only
        validateCollision = false;
        ActivateRPC();
        Debug.Log("Something Detected");
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false, Delivery = RpcDelivery.Unreliable)]
    public void ActivateRPC()
    {
        validateCollision = true;
        
    }

    public void Cooldown()
    {
        if (!readyToFire)
        {
            cdTimer -= Time.deltaTime;
            if(cdTimer < 0)
            {
                readyToFire=true;
            }
            
        }
    }
}
