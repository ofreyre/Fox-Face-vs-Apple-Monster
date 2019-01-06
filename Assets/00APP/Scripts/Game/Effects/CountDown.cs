using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    public Text m_text;
    public int m_start = 3;
    public float m_numberDuration = 1;
    float m_t;

	// Use this for initialization
	void Start () {
        m_t = Time.time + m_numberDuration;
        m_text.text = m_start.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_t > Time.time)
        {
            float k = (m_t - Time.time) / m_numberDuration;
            transform.localScale = new Vector3(k, k,1);
        }
        else
        {
            if (m_start > 1)
            {
                m_start--;
                m_text.text = m_start.ToString();
                m_t = Time.time + m_numberDuration;
            }
            else
            {
                GameEvents.DispatchFlowEvent(FLOWEVENTTYPE.start);
                gameObject.SetActive(false);
            }
        }
	}
}
