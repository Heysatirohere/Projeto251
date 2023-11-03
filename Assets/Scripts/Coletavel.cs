using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coletavel : MonoBehaviour
{


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            ColetavelManager.instance.item--;
            Destroy(gameObject);

        }
        print(other.gameObject.name);


    }


}
