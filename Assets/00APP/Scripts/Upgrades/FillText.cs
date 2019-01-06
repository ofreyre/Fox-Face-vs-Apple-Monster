using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillText : MonoBehaviour {
    public Text m_text;

    public void Fill(string text) {
        m_text.text = text;
    }
}
