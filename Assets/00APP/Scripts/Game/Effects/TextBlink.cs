using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour {
    public Text m_text;
    public float m_visibleDuration = 1;
    public float m_invisibleDuration = 0.5f;
    float m_t;
    bool m_isVisible;

    // Use this for initialization
    void OnEnable()
    {
        m_isVisible = true;
        m_t = Time.time + m_visibleDuration;
        m_text.enabled = true;
    }

    void OnDisable()
    {
        m_text.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > m_t)
        {
            if (m_isVisible)
            {
                m_isVisible = false;
                m_text.enabled = false;
                m_t = Time.time + m_invisibleDuration;
            }
            else
            {
                m_isVisible = true;
                m_text.enabled = true;
                m_t = Time.time + m_visibleDuration;
            }
        }
    }
}
