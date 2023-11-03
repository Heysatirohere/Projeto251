using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float walkSpeed;
    public float sprintSpeed;
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public float jumpForce;
    public float jumpCoolDown = 0.1f;
    public float multiplier;
    bool ReadytoJump = true; 
    //////
    public float groundDrag;
    public float playerHeight; 
    public LayerMask whatIsGround;
    public bool grounded;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float WallRunSpeed; 
    public MovementState state;
    private WallRunning wr;
    public int Jumps = 2;
    public enum MovementState
    {
        walking,
        sprinting, 
        wallrunning

    }

    public bool Wallrunning; 

    void Start()
    {
        wr = GetComponent<WallRunning>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void StateHandler()
    {
        if (grounded && !Input.GetKey(sprintKey))
            {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            


        }
       else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        
        if (Wallrunning)
        {
            state = MovementState.wallrunning;
            moveSpeed = WallRunSpeed; 
           
        }

    }
    void MovePlayer()
    {
 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * multiplier, ForceMode.Force);

    }

    private void Update()
    {

        SpeedControl();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
        {
            rb.drag = groundDrag;
            if (ReadytoJump)
            {
                Jumps = 2;
            }
        }
            
        else rb.drag = 0;
        if (Input.GetButtonDown("Jump") && ReadytoJump && Jumps > 0)
        {

            ReadytoJump = false;
            Jump();
            Invoke("ResetJump", jumpCoolDown);
        }
        if (Input.GetButtonDown("Jump") && Wallrunning)
        {
            WallRunningJump();
        }

        StateHandler();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);



        }
    }

    private void Jump()
    {
        Jumps--;
        if (Jumps < 2) rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void WallRunningJump()
    {
        if (wr.wallRight)
            rb.AddForce (-orientation.right * 170, ForceMode.Impulse);
        if (wr.wallLeft)
            rb.AddForce(orientation.right * 170, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        ReadytoJump = true; 
    }
   
}
