using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceY : MonoBehaviour
{
    public float m_duration = 0.2f;
    float t;

    // Use this for initialization
    void OnEnable()
    {
        t = Time.time + m_duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < Time.time)
        {
            gameObject.SetActive(false);
        }
    }
}
