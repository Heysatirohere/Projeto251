using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player") && ColetavelManager.instance.item == 0) 
        {
           
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            LoadManager.Instance.Load("WinScene");
        }

    }


        void Update()
        {



        }
    }


