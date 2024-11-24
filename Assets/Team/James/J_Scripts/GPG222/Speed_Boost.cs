using JamesKilpatrick;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Speed_Boost : MonoBehaviour
{
    [SerializeField] private AudioSource speedBoost;
    [SerializeField] private AudioSource speedDown;
    public float speedIncrease = 10f;
    public Renderer speedBoostRenderer;
    public List<Rigidbody> objectsInSpeedBoost = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Rigidbody body in objectsInSpeedBoost)
        {
            //Makes any object with a rigidbody that enters faster.
            body.AddForce(transform.forward * speedIncrease * Time.deltaTime);
        }
    }

    public void ActivateSpeedBoost()
    {
        //Play sound when entering
        speedBoost.Play();

        //Change Speed Boost color for all clients when marble enter the speed boost.
        ChangeSpeedColorOnClientsRPC(Color.cyan);
    }

    public void DeactiviateSpeedBoost()
    {
        //Play sound when leaving.
        speedDown.Play();

        //Change Speed Boost color for all clients when marble leaves the speed boost.
        ChangeSpeedColorOnClientsRPC(Color.red);
    }


    public void OnTriggerEnter(Collider other)
    {
        //Adds any object with a rigidbody to the objectsInSpeedBoost list
        objectsInSpeedBoost.Add (other.gameObject.GetComponent<Rigidbody>());

        //If player enters play sound effect
        if (other.CompareTag("Player"))
        {
            ActivateSpeedBoost();
        }

    }

    public void OnTriggerExit(Collider other)
    {
        //Removes any object with a rigidbody in the objectsInSpeedBoost list
        objectsInSpeedBoost.Remove (other.gameObject.GetComponent<Rigidbody>());
        
        //If player leaves play sound effect
        if (other.CompareTag("Player"))
        {
            DeactiviateSpeedBoost();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void ChangeSpeedColorOnClientsRPC(Color color)
    {
        speedBoostRenderer.material.color = color;
    }
}
