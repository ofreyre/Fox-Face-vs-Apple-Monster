using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour {
    public float m_duration;
    WaitForSeconds m_lapse;

    void OnEnable()
    {
        if (m_lapse == null)
        {
            m_lapse = new WaitForSeconds(m_duration);
        }
        StartCoroutine(Live());
    }

    IEnumerator Live()
    {
        yield return m_lapse;
        gameObject.SetActive(false);
    }
}
