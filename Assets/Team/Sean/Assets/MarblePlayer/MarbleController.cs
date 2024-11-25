using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class MarbleController : NetworkBehaviour
{
    //Customizable variables
    public float playerSpeed = 5f;
    public float jumpForce = 100f;
    [SerializeField] private bool useOffline = false;

    //input variables
    private float xInput;
    private float yInput;

    //gameobj variables
    public Vector3 moveDir;
    
    private Rigidbody rb;
    private MarbleNetworkconnect network;
    private MarbleControllerView marbleView;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        marbleView = GetComponent<MarbleControllerView>();

        if (!useOffline)
        {
            network = GetComponent<MarbleNetworkconnect>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner || useOffline)
        {
            PlayerInputs();
        }
    }

    private void FixedUpdate()
    {
        if (IsOwner || useOffline)
        {
            Movement();
        }
    }

    private void PlayerInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
        if (!useOffline)
        {
            network.reqJumpValidationRPC();
        }
        else
        {
            marbleView.JumpEffects();
        }
    }

    private void Movement()
    {
        moveDir = Vector3.forward * yInput + Vector3.right * xInput;
        rb.AddForce(moveDir.normalized * playerSpeed, ForceMode.Force);
    }

    
}
