using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;

public class WallRunning : MonoBehaviour
{
 public LayerMask Wall;
 public LayerMask Ground;
    public float wallRunForce;
    public float wallRunTime;
    public float runTimer;
    public float WallClimb; 
    private float horizontalinput;
    private float verticalinput;

    public float wallCheckDistance;
    public float jumpHeight;
    private RaycastHit leftWall;
    private RaycastHit rightWall;
    public bool wallLeft; 
    public bool wallRight;

    public Transform orientation;
    private Movement pm;  
    private Rigidbody rb;

    public KeyCode upwardRunKey = KeyCode.LeftShift;
    public KeyCode downwardRunKey = KeyCode.LeftControl;
    private bool upwardRunning;
    private bool downwardRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Movement>();
    }

    private void CheckWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWall, wallCheckDistance, Wall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWall, wallCheckDistance, Wall);
    }

    private bool aboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, jumpHeight, Ground);

    }

    public void input ()
    {
        upwardRunning = Input.GetKey(upwardRunKey);
        downwardRunning = Input.GetKey(downwardRunKey);


        horizontalinput = Input.GetAxisRaw("Horizontal");
        verticalinput = Input.GetAxisRaw("Vertical");

        if ((wallLeft || wallRight) && verticalinput > 0 && aboveGround())
        {
            if (!pm.Wallrunning)
                RunningWall();
            print("Correndo");
        } 
        else
        {
            if (pm.Wallrunning)
                StoppedRun();
            print("não correndo");
        }
    }
    
    private void Run()
    {
        pm.Wallrunning = true;
    }

private void StoppedRun ()
    {

        pm.Wallrunning = false;
        rb.useGravity = true; 
    } 

    private void RunningWall()
    {
     

        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWall.normal : leftWall.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        if (!(wallLeft && horizontalinput > 0) && !(wallRight && horizontalinput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);

        if (upwardRunning)
            rb.velocity = new Vector3(rb.velocity.x, WallClimb, rb.velocity.z);
        if (downwardRunning)
            rb.velocity = new Vector3(rb.velocity.x, -WallClimb, rb.velocity.z);
        Run();
    }

    private void FixedUpdate()
    {
        if (pm.Wallrunning)
            RunningWall();

    }

    void Update()
    {
        CheckWall();
        input();
    }
}
