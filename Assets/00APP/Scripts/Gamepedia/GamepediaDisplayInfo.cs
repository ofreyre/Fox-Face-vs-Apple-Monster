using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepediaDisplayInfo : MonoBehaviour {
    public GamepediaBank m_bank;
    public Transform m_thumbnailContainer;
    public Transform m_descriptionContainer;
    public Text m_name;
    GameObject m_thumbnail;
    GameObject m_description;

    void Start ()
    {
        GamepediaEvents.instance.AttackerSelected += OnAttackerSelected;
        GamepediaEvents.instance.UnitSelected += OnUnitSelected;
    }
	
	void OnAttackerSelected(ATTACKERTYPE type) {
        if (type != ATTACKERTYPE.none)
        {
            GamepediaAttackerBankItem item = m_bank.GetAttacker(type);
            Display(item.prefab, item.description, item.name);
        }
        else
        {
            Clear();
        }
    }

    void OnUnitSelected(UNITTYPE type)
    {
        if (type != UNITTYPE.none)
        {
            GamepediaDefenderBankItem item = m_bank.GetDefender(type);
            Display(item.prefab, item.description, item.name);
        }
        else
        {
            Clear();
        }
    }

    public void Display(GameObject thumbnail, GameObject description, string name)
    {
        Clear();

        m_thumbnail = Instantiate(thumbnail);
        m_thumbnail.transform.SetParent(m_thumbnailContainer);
        m_thumbnail.transform.localScale = Vector3.one;
        m_thumbnail.transform.localPosition = Vector3.zero;
        m_description = Instantiate(description);
        m_description.transform.SetParent(m_descriptionContainer);
        m_description.transform.localScale = Vector3.one;
        m_description.transform.localPosition = Vector3.zero;
        m_descriptionContainer.GetComponent<ScrollRect>().content = m_description.transform.GetComponent<RectTransform>();

        m_name.text = name;
    }

    void Clear()
    {
        if (m_thumbnail != null)
        {
            Destroy(m_thumbnail);
            m_thumbnail = null;
        }

        if (m_description != null)
        {
            Destroy(m_description);
            m_description = null;
        }
    }
}
