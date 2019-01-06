using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRemove : MonoBehaviour {

    public BlinkImage m_blink;

	void Start ()
    {
        m_blink.enabled = false;
        GameEvents.instance.InventoryClicked += OnInventoryClicked;
    }
	
	public void OnClick () {
        if (!m_blink.enabled)
        {
            GameEvents.instance.ClickToRemove += OnClickToRemove;
        }
        else
        {
            GameEvents.instance.ClickToRemove -= OnClickToRemove;
        }
        m_blink.enabled = !m_blink.enabled;
        GameEvents.DispatchRemoveClicked();
    }

    void OnInventoryClicked()
    {
        GameEvents.instance.ClickToRemove -= OnClickToRemove;
        m_blink.enabled = false;
    }

    void OnClickToRemove(Vector2 screenpos)
    {
        GameEvents.instance.ClickToRemove -= OnClickToRemove;
        m_blink.enabled = false;
    }
}
