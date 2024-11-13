using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpForce = 10f;
    public AudioSource jumpPad;
    public Animator jumpPadani;

    private void Start()
    {
        jumpPad = GetComponent<AudioSource>();
        jumpPadani = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player_rb = other.GetComponent<Rigidbody>();

            if (player_rb != null)
            {
                jumpPad.Play();
                jumpPadani.SetTrigger("JumpPadTrigger");

                player_rb.velocity = new Vector3 (player_rb.velocity.x, jumpForce, player_rb.velocity.z); 

                Debug.Log("Player Jumped");
            }
        }
    }


}
