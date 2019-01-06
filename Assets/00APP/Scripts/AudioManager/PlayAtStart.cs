using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class PlayAtStart : MonoBehaviour {
    public string m_audio;
    public bool loop;

    void Start() {
        AudioManager.instance.Play(m_audio.GetHashCode());
    }
}
