using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    void Awake()
    {
        Instance = this;

    }

    public void Load(string scene)
    {

        SceneManager.LoadScene(scene);

    }
}
