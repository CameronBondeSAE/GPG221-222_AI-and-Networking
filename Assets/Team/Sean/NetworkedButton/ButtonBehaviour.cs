using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

public class ButtonBehaviour : NetworkBehaviour
{

    //[SerializeField] Collider detectArea;

    //[SerializeField] bool interactable = false;
    //[SerializeField] bool toggle = true;

    List <GameObject> currentCollisions = new List <GameObject>();

    private bool onButtonDown = false;
    private bool activeButton = false;

    public UnityEvent<bool> thingToToggle;
    public UnityEvent thingToDo;

    //Check if there is nothing on the button
    private void OnTriggerEnter(Collider collision)
    {
        //Should trigger if the button is reset and nothing is on it
        if (!onButtonDown)
        {
            Trigger_RequestToServer_Rpc();
        }
        currentCollisions.Add(collision.gameObject);
        
    }

    private void OnTriggerExit(Collider collision)
    {
        currentCollisions.Remove(collision.gameObject);
        
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCollisions.Count <= 0)
        {
            onButtonDown = false;
        }
        else
        {
            onButtonDown = true;
        }

        

    }

    



    //RPC area

    //To server
    private void Trigger_RequestToServer_Rpc()
    {
        ToggleRPC();
    }

    //Server Validate + update
    public void ToggleRPC()
    {

        activeButton =!activeButton;
        thingToToggle.Invoke(activeButton);
    }


    //Check for things on the button
    //If there is nothing on the button reset the button
    //If there are things on the button, check the button reset
    //If the button reset 
}

