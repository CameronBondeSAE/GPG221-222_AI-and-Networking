using JamesKilpatrick;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Speed Boost Game Object
/// This is a area that will increase any marbles speed when inside of its area
/// It does this by uses a list of rigibodies and a on trigger enter and exit
/// Entering the trigger area adds the marbles rb to the list which increases speed
/// Exiting the trigger area removes the marbles rb from the list which removes the speed increase
/// It uses material renders that are networked to show all players if people are in the speed boost
/// </summary>
public class Speed_Boost : NetworkBehaviour
{
    [SerializeField] private AudioSource speedBoostAudio;
    [SerializeField] private AudioSource speedDownAudio;
    [Tooltip("Float to change the amount of speed increase player gets")]
    public float speedIncrease = 50f;
    [Tooltip("Reference to renderer to show color change")]
    public Renderer speedBoostRenderer;
    [Tooltip("Creates a list of rigidbody when in speed boost to allow multiple marbles to go fast")]
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
        if (IsServer)
        {
            //Change Speed Boost color and Play sound for all clients when marble enter the speed boost.
            ActivateSpeedBoostRPC(Color.cyan);

            PlaySpeedBoostSoundRPC();
        }

    }

    public void DeactiviateSpeedBoost()
    {
        if (IsServer)
        {
            //Change Speed Boost color and play speed down sound for all clients when marble leaves the speed boost.
            ActivateSpeedBoostRPC(Color.red);

            PlaySpeedDownSoundRPC();
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        //Adds any object with a rigidbody to the objectsInSpeedBoost list
        objectsInSpeedBoost.Add(other.gameObject.GetComponent<Rigidbody>());

        //If player enters play sound effect
        if (other.CompareTag("Player"))
        {
            ActivateSpeedBoost();
        }

    }

    public void OnTriggerExit(Collider other)
    {
        //Removes any object with a rigidbody in the objectsInSpeedBoost list
        objectsInSpeedBoost.Remove(other.gameObject.GetComponent<Rigidbody>());

        //If player leaves play sound effect
        if (other.CompareTag("Player"))
        {
            DeactiviateSpeedBoost();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void ActivateSpeedBoostRPC(Color color)
    {
        speedBoostRenderer.material.color = color;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void PlaySpeedBoostSoundRPC()
    {
        speedBoostAudio.Play();
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void PlaySpeedDownSoundRPC()
    {
        speedDownAudio.Play();
    }
}
