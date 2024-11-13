using JamesKilpatrick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Boost : MonoBehaviour
{
    [SerializeField] private AudioSource speedBoost;
    [SerializeField] private AudioSource speedDown;
    public float speedIncrease = 10f;

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
            //Makes any object with a rigidbody that enters faster
            body.AddForce(transform.forward * speedIncrease * Time.deltaTime);
        }
    }

    public void ActivateSpeedBoost()
    {
        //Play sound when entering
        speedBoost.Play();
    }

    public void DeactiviateSpeedBoost()
    {
        //Play sound when leaving
        speedDown.Play();
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
}
