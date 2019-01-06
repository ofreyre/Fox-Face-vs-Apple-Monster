using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public float m_visibleDuration = 1;
    public float m_invisibleDuration = 0.5f;
    float m_t;
    bool m_isVisible;
    SpriteRenderer m_renderer;

    void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void OnEnable () {
        m_isVisible = true;
        m_t = Time.time + m_visibleDuration;
        m_renderer.enabled = true;
    }

    // Update is called once per frame
    void Update () {

        if (Time.time > m_t)
        {
            if (m_isVisible)
            {
                m_isVisible = false;
                m_renderer.enabled = false;
                m_t = Time.time + m_invisibleDuration;
            }
            else
            {
                m_isVisible = true;
                m_renderer.enabled = true;
                m_t = Time.time + m_visibleDuration;
            }
        }
	}
}
