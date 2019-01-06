using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBankItemClick : MonoBehaviour {
    UNITTYPE m_type;
    public Button m_btn;
    public Transform m_recover;
    public GameObject m_selected;
    float m_t;
    Coroutine m_Recover;

    void Start()
    {
        m_type = GetComponent<UnitType>().type;
    }

    public void OnClick()
    {
        ItemsEvents.DispatchUnitClick(m_type);
    }

    public void Enable()
    {
        if (!m_btn.interactable)
        {
            m_btn.interactable = true;
            m_recover.gameObject.SetActive(false);
            if (m_Recover != null)
            {
                StopCoroutine(m_Recover);
            }
        }
    }

    public void Disable()
    {
        m_btn.interactable = false;
        //m_recover.gameObject.SetActive(false);
        m_selected.SetActive(false);
        /*if (m_Recover != null)
        {
            StopCoroutine(m_Recover);
        }*/
    }

    public void Recover(float duration)
    {
        if (m_btn.interactable)
        {
            m_btn.interactable = false;
            m_recover.gameObject.SetActive(true);
            m_selected.SetActive(false);
            m_recover.localScale = Vector3.one;
            m_Recover = StartCoroutine(_Recover(duration));
        }
    }

    IEnumerator _Recover(float duration)
    {
        float t = Time.time + duration;
        while (Time.time < t)
        {
            m_recover.localScale = new Vector3(1, (t - Time.time) / duration, 1);
            yield return null;
        }
        GameEvents.DispatchUnitItemRecovered(m_type);
    }
}
