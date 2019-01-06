using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnItem : MonoBehaviour
{
    public enum btnItem_State
    {
        disabled,
        enabled,
        used
    }

    public GameObject m_frame_selected;
    public Image m_icon;
    public Color m_colorDisabled;
    public Color m_colorEnabled;
    public Color m_colorSelected;
    public Color m_colorUsed;
    btnItem_State m_state;
    protected bool consumable;

    public btnItem_State State
    {
        set
        {
            m_state = value;
            switch (value)
            {
                case btnItem_State.disabled:
                    Disabled();
                    break;
                case btnItem_State.enabled:
                    Enabled();
                    break;
                case btnItem_State.used:
                    Used();
                    break;

            }
        }

        get {
            return m_state;
        }
    }

    void Disabled()
    {
        m_icon.color = m_colorDisabled;
        m_frame_selected.SetActive(false);
        consumable = false;
    }

    void Enabled()
    {
        m_icon.color = m_colorEnabled;
        m_frame_selected.SetActive(false);
        consumable = true;
    }

    void Used()
    {
        m_icon.color = m_colorUsed;
        m_frame_selected.SetActive(false);
        consumable = false;
    }

    public void Select()
    {
        //m_frame.color = m_colorSelected;
        m_frame_selected.SetActive(true);
    }

    public void UnSelect()
    {
        State = m_state;
        m_frame_selected.SetActive(false);
    }
}
