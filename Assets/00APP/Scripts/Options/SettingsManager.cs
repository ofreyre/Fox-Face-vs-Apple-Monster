using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    public GlobalFlow m_flow;
    public string m_sceneGDRP;
    public string m_sceneOK;
    public string m_sceneCredits;

    void Start () {
        OptionsManager.instance.GDRP += OnGDRP;
        OptionsManager.instance.OK += OnOK;
        OptionsManager.instance.Credits += OnCredits;
    }
	
	void OnGDRP()
    {
        m_flow.ToScene(m_sceneGDRP);
    }

    void OnOK()
    {
        m_flow.ToScene(m_sceneOK);
    }

    void OnCredits()
    {
        m_flow.ToScene(m_sceneCredits);
    }
}
