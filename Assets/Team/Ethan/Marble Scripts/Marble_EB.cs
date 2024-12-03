using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

/// <summary>
/// This script is the player marble script. It handles its movement and makes sure only the owner can control the marble. 
/// It sends to the server its transform position, rotation, velocity and angularvelocity so it links up with other clients.
/// It applies force to the marble through the rigidbody and tracks its location using a Vector3.
/// </summary>
public class Marble_EB : NetworkBehaviour
{
    // Variables
    // They store information
    // This one stores which 'Rigidbody' component we want to talk to. They do the physics movement
    [Tooltip("Rigidbody of marble for movement")]
    [SerializeField] private Rigidbody rb;
    // This one stores a 'float' which is just a number. You can change these in the editor
    [Tooltip("FLoat for the speed of the marble")]
    [SerializeField] private float speed = 25f;

    [SerializeField] private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 maxScale = new Vector3(1f, 1f, 1f);

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private Vector3 marbleVelocity;
    private Vector3 marbleAngularVelocity;

    private void Start()
    {
        if (IsOwner)
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            marbleAngularVelocity = rb.angularVelocity;
            marbleVelocity = rb.velocity;
        }

    }

    // Functions
    // This gets run by Unity whenever the screen updates
    private void Update()
    { 
        if (IsOwner)
        {
            // We're talking to the Rigidbody via our variable. Note the dot. This will show you everything that component can do
            rb.AddTorque(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);

            if (Vector3.Distance(transform.position, lastPosition) > 0.01f || Quaternion.Angle(transform.rotation, lastRotation) > 0.05f)
            {
                // Pass the position, rotation, velocity, and angular velocity
                UpdateTransformServerRpc(transform.position, transform.rotation, rb.velocity, rb.angularVelocity);

                lastPosition = transform.position;
                lastRotation = transform.rotation;
            }
            scaleCharacter();
        } 
    }

    private void scaleCharacter()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            scaleCharacterOnServerRpc(minScale);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            scaleCharacterOnServerRpc(maxScale);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void scaleCharacterOnServerRpc(Vector3 newScale, ServerRpcParams rpcParams = default)
    {
        transform.localScale = newScale;

        scaleOnClientRpc(newScale);

    }

    [ClientRpc]
    private void scaleOnClientRpc(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateTransformServerRpc(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity, ServerRpcParams rpcParams = default)
    {
        transform.position = position;
        transform.rotation = rotation;

        // Broadcast updated state to all clients
        UpdateTransformClientRpc(position, rotation, velocity, angularVelocity);
    }

    [ClientRpc]
    private void UpdateTransformClientRpc(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
    {
        if (!IsOwner)
        {
            transform.position = position;
            transform.rotation = rotation;
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;
        }
    }

}
