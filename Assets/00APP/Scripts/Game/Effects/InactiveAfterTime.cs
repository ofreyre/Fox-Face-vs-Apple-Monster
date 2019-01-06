using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveAfterTime : MonoBehaviour
{
    public float m_duration = 1;
    float m_t;

    // Use this for initialization
    void OnEnable ()
    {
        m_t = Time.time + m_duration;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > m_t)
        {
            gameObject.SetActive(false);
        }
	}
}
