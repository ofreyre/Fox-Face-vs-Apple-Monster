using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public float m_visibleDuration = 1;
    public float m_invisibleDuration = 0.5f;
    float m_t;
    bool m_isVisible;
    Image m_image;
    Color m_colorNormal = new Color(1, 1, 1, 0);
    Color m_colorBlink = new Color(1,1,1,0);

    void Awake()
    {
        m_image = GetComponent<Image>();
        m_colorNormal = m_image.color;
    }

    void OnEnable()
    {
        m_isVisible = true;
        m_t = Time.time + m_visibleDuration;
        m_image.color = m_colorNormal;
    }
    void OnDisable()
    {
        m_isVisible = true;
        m_image.color = m_colorNormal;
    }

    void Update()
    {

        if (Time.time > m_t)
        {
            if (m_isVisible)
            {
                m_isVisible = false;
                m_image.color = m_colorBlink;
                m_t = Time.time + m_invisibleDuration;
            }
            else
            {
                m_isVisible = true;
                m_image.color = m_colorNormal;
                m_t = Time.time + m_visibleDuration;
            }
        }
    }
}
