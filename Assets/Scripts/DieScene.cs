using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            LoadManager.Instance.Load("Menu"); //
        }
        
    }
}
