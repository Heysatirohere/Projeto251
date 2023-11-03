using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    void Awake()
    {
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
