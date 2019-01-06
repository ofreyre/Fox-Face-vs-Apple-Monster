using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateStars : MonoBehaviour
{
    public GameStats m_gameStats;
    public GameObject[] m_stars;
    public float m_interval = 1;
    int i, n;
    float m_t;

	public void Init ()
    {
        n = m_gameStats.Stars;
        if (n > 0)
        {
            i = 0;
            enabled = true;
            m_t = Time.time + m_interval;
            m_stars[i].SetActive(true);
            enabled = true;
        }
    }
	
	void Update ()
    {
        if (Time.time > m_t)
        {
            i++;
            if (i < n)
            {
                m_t = Time.time + m_interval;
                m_stars[i].SetActive(true);
            }
            else
            {
                enabled = false;
                GameEndFlow.DispatchStepEnd();
            }
        }
	}

    public void Fill()
    {
        for (int i = 0, n = m_gameStats.Stars; i < n; i++)
        {
            m_stars[i].GetComponent<MoveToTarget>().End();
            m_stars[i].SetActive(true);
        }
    }
}
