using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource source;  
    public AudioClip jumpSFX;
    void Start()
    {
        
        source = GetComponent<AudioSource>();


    }

    public void PlayAudio (AudioClip clip)
    {
        source.PlayOneShot(clip);

    }
    void Update()
    {
        
    }
}
