using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlay : MonoBehaviour {
    public GlobalFlow m_flow;
    public string m_prevScene = "Stage";
    public bool m_ifEqual = true;

    // Use this for initialization
    void Start () {
        gameObject.SetActive((m_flow.PrevScene == m_prevScene) == m_ifEqual);
	}
}
