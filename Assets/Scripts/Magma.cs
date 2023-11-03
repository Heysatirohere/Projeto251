using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    
    
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            LoadManager.Instance.Load("Die");

        }
    }


    void Update()
    {
        if (ColetavelManager.instance.timeSec <= 0) 
            transform.position = new Vector3(0, transform.position.y + Time.deltaTime * 3, 0);     
    }
}
