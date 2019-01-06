using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class BtnPlaySFX : MonoBehaviour {

    public string m_clip;
    int clipKey;

    private void Awake()
    {
        clipKey = m_clip.GetHashCode();
    }

    public void Play() {
        AudioManager.instance.Play(clipKey);
    }
}
