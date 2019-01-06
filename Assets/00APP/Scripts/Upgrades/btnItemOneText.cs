using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnItemOneText : btnItem
{

    public FillText m_text;

    public void Fill(string text) {
        m_text.Fill(text);
    }
}
