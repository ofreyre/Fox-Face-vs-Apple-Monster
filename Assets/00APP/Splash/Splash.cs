using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    public string m_nextScene;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadGame());
	}

    IEnumerator LoadGame() {
        AnimationEvent e = new AnimationEvent();
        e.functionName = "PlaySound";
        e.time = 0f/60f;
        GetComponent<Animator>().runtimeAnimatorController.animationClips[0].AddEvent(e);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(m_nextScene);
    }

    void PlaySound() {
        GetComponent<AudioSource>().Play();
    }
}
