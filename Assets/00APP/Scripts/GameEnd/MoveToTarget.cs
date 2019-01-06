using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {

    public Transform m_target;
    public float m_duration = 1;
    Vector3 m_pos, m_scale;
    float m_t;

	// Use this for initialization
	void Start () {
        m_pos = transform.position;
        m_scale = transform.localScale;
        m_t = Time.time + m_duration;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time < m_t)
        {
            float k = (m_t - Time.time) / m_duration;
            transform.position = m_pos * k + m_target.transform.position * (1 - k);
            transform.localScale = m_scale * k + m_target.transform.localScale * (1 - k);
        }
        else
        {
            transform.position = m_target.transform.position;
            transform.localScale = m_target.transform.localScale;
            enabled = false;
        }
    }

    public void End()
    {
        transform.position = m_target.transform.position;
        transform.localScale = m_target.transform.localScale;
        enabled = false;
    }
}
