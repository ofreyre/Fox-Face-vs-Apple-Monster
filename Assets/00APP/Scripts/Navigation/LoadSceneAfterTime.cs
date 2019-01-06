using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterTime : MonoBehaviour {

    public float duration = 3;
    public string scene = "Main";

    // Use this for initialization
    void Start () {
        Invoke("Load", duration);
    }

    void Load()
    {
        SceneManager.LoadScene(scene);
    }
}
