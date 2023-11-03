using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ColetavelManager : MonoBehaviour
{
    public int item = 5; 
    public static ColetavelManager instance;
    public Text CountItem;
    public Text count;
    public float timeSec = 150;

    void Awake()
    {
        
       instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        CountItem.text = item.ToString() + "/5";
        timeSec -= Time.deltaTime;
        int min = (int)timeSec / 60;
        int sec = (int)timeSec % 60;
        count.text = string.Format("{0}:{1:00}", min, sec);
    }
  }
