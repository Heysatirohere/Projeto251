using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    public float verticalInput;
    Vector3 moveDirection;
    Rigidbody2D rb; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
