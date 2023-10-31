using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public float yRotation;
    public Transform CamPosition; 
    public float xRotation;

    public Transform orientation; 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CamPosition.position;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * 100f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * 100f;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler (xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
