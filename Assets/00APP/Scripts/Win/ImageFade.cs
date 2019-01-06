using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {

    public float m_wait = 5;
    public float duration = 1;
    public Color m_start;
    public Color m_end;
    Image m_image;

    void Start () {
        m_image = GetComponent<Image>();
        m_image.color = m_start;
        StartCoroutine(Run());
    }
	
	IEnumerator Run()
    {
        yield return new WaitForSeconds(m_wait);
        float t = Time.time + duration;
        float k, d = 1/duration;
        while (Time.time < t)
        {
            k = (t - Time.time) * d;
            m_image.color = m_start * k + (1 - k) * m_end;
            yield return null;
        }
        m_image.color = m_end;
    }
}
