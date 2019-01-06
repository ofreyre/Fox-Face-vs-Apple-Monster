using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEarnCoinsText : MonoBehaviour {
    public Globals m_globals;
    public Text m_text;
    public Text m_reward;

	// Use this for initialization
	void Start () {
        m_text.text = "Earn " + m_globals.coinsPerVideo + " cons!\nWatch a video";
        if (m_reward != null)
        {
            m_reward.text = m_globals.coinsPerVideo.ToString();
        }
    }
}
