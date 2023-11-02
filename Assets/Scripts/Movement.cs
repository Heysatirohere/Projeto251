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

    public MovementState state; 

    public enum MovementState
    {
        walking,
        sprinting, 
        air

    }

    void Start()
    {
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
            print(!Input.GetKey(sprintKey));


        }
       else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
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
        speedControl();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
            rb.drag = groundDrag;
        else rb.drag = 0;
        if (Input.GetButtonDown("Jump") && ReadytoJump && grounded)
        {

            ReadytoJump = false;
            Jump();
            Invoke("ResetJump", jumpCoolDown);
        }
        StateHandler();
    }

    private void speedControl()
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
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        ReadytoJump = true; 
    }
   
}
