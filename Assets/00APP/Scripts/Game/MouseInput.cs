using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {

    public float m_removeClickDelay = 1;
    bool m_destroying;
    float m_removeClickT;

    // Use this for initialization
    public void Init () {
        GameEvents.instance.RemoveClicked += OnRemoveClicked;
        GameEvents.instance.InventoryClicked += OnInventoryClicked;
    }

    public void Begin()
    {
        enabled = true;
    }

    public void End()
    {
        enabled = false;
    }

    void OnRemoveClicked()
    {
        m_destroying = !m_destroying;
        m_removeClickT = Time.time + m_removeClickDelay;
    }

    void OnInventoryClicked()
    {
        m_destroying = false;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 v = Input.mousePosition;
            Vector2 screenpos = new Vector2(v.x, v.y);
            if (!m_destroying)
            {
                if (!BabyPacmanSpawner.instance.OnMapClicked(screenpos))
                {
                    GameEvents.DispatchMapClicked(screenpos);
                }
            }
            else if(Time.time > m_removeClickT)
            {
                m_destroying = false;
                GameEvents.DispatchClickToRemove(screenpos);
            }
        }
	}
}
