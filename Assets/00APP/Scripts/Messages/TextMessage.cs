using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMessage : MonoBehaviour {
    
    public GameObject m_btnOK;
    public GameObject m_btnYes;
    public GameObject m_btnNo;
    public GameObject m_btnRemove;
    public GameObject m_btnWatch;

    public bool m_displayOK = true;
    public bool m_displayYes;
    public bool m_displayNo;
    public bool m_displayRemove;
    public bool m_displayWatch;

    public bool m_closeOnButton;


    public Text m_text;
    
    void Start()
    {
        EventManagerMessages.instance.Message += SetText;
        gameObject.SetActive(false);
        m_btnOK.SetActive(m_displayOK);
        m_btnYes.SetActive(m_displayYes);
        m_btnNo.SetActive(m_displayNo);
        m_btnRemove.SetActive(m_displayRemove);
        m_btnWatch.SetActive(m_displayWatch);
    }

    void SetText(string text, bool ok, bool yes, bool no, bool remove, bool watch)
    {
        m_btnOK.SetActive(ok);
        m_btnYes.SetActive(yes);
        m_btnNo.SetActive(no);
        m_btnRemove.SetActive(remove);
        m_btnWatch.SetActive(watch);

        m_text.text = text;
        gameObject.SetActive(true);
    }

    public void OnOK()
    {
        if (m_closeOnButton)
            gameObject.SetActive(false);
        EventManagerMessages.instance.DispatchOK();
    }

    public void OnYes()
    {
        if (m_closeOnButton)
            gameObject.SetActive(false);
        EventManagerMessages.instance.DispatchYes();
    }

    public void OnNo()
    {
        if (m_closeOnButton)
            gameObject.SetActive(false);
        EventManagerMessages.instance.DispatchNo();
    }

    public void OnRemove()
    {
        if (m_closeOnButton)
            gameObject.SetActive(false);
        EventManagerMessages.instance.DispatchRemove();
    }

    public void OnWatch()
    {
        if (m_closeOnButton)
            gameObject.SetActive(false);
        EventManagerMessages.instance.DispatchWatch();
    }
}
