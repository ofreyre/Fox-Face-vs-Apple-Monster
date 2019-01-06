using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reduce : MonoBehaviour {

    public float m_duration = 0.5f;
    float m_durationInv;
    Vector3 m_scale;
    float m_t;

    void Awake()
    {
        m_durationInv = 1 / m_duration;
        m_scale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localScale = m_scale;
        m_t = Time.time + m_duration;
    }
    
    void Update () {
        if (Time.time < m_t)
        {
            transform.localScale = m_scale * (m_t - Time.time) * m_durationInv;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
