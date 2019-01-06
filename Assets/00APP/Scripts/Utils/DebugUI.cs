using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {

    public Text m_text;

    public static DebugUI instance;

    private void Awake()
    {
        instance = this;
    }

    public static void Log (string line) {
        if (instance != null)
        {
            instance.m_text.text += "\n" + line;
        }
	}
}
