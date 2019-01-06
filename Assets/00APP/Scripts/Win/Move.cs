using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float m_wait = 6;
    public float duration = 5;
    public Vector3 m_start;
    public Vector3 m_end;

    void Start()
    {
        gameObject.transform.localPosition = m_start;
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(m_wait);
        float t = Time.time + duration;
        float k, d = 1 / duration;
        while (Time.time < t)
        {
            k = (t - Time.time) * d;
            gameObject.transform.localPosition = m_start * k + (1 - k) * m_end;
            yield return null;
        }
        gameObject.transform.localPosition = m_end;
    }
}
