using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class JumpPad : NetworkBehaviour
{
    public float jumpForce = 10f;
    public AudioSource jumpPad;
    public Animator jumpPadani;
    public Renderer jumpPadR;

    private bool animationPlayed = false;

    private void Start()
    {
        jumpPad = GetComponent<AudioSource>();
        jumpPadani = GetComponent<Animator>();
        jumpPadR = GetComponent<Renderer>();

        jumpPadR.material.color = Color.cyan;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsServer)
            {
                // Handle force directly for host
                if (other.GetComponent<NetworkObject>().IsOwner)
                {
                    ApplyForce(other);
                }
                else
                {
                    // Notify the client to apply force locally
                    TriggerJumpClientRpc(other.GetComponent<NetworkObject>().OwnerClientId);
                }

                // Update visuals for everyone
                ChangeVisualsClientRpc(Color.green, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsServer)
            {
                ResetJumpPad();
            }
        }
    }

    private void ApplyForce(Collider player)
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
        }
    }

    [ClientRpc]
    private void TriggerJumpClientRpc(ulong clientId)
    {
        // Ensure only the correct client applies the force
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            if (playerObject != null)
            {
                Rigidbody playerRb = playerObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
                }
            }
        }
    }

    private void ResetJumpPad()
    {
        Debug.Log("Player Left");
        ChangeVisualsClientRpc(Color.cyan, false);
    }

    [ClientRpc]
    private void ChangeVisualsClientRpc(Color color, bool playAnimation)
    {
        jumpPadR.material.color = color;

        if (playAnimation && !animationPlayed)
        {
            jumpPad.Play();
            jumpPadani.SetTrigger("JumpPadTrigger");
            animationPlayed = true;
        }
        else if (!playAnimation)
        {
            animationPlayed = false;
        }
    }
}
