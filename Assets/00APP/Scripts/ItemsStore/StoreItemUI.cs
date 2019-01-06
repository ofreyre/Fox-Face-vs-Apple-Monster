using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemUI : ItemUI
{
    public GameObject m_frameSelected;
    public Color m_colorDisabled;
    public Text m_priceUI;
    public Color m_colorTextNormal;
    public Color m_colorTextSelected;
    public bool m_consumable;

    public override void Fill(DBinventoryItem item, int amount)
    {

        //m_frameSelected.transform.localScale = new Vector3(134.0f / 96, 1, 1);
        //m_frameSelected.GetComponent<RectTransform>().sizeDelta = new Vector2(134f, 64);
        m_priceUI.text = item.price.ToString();
        base.Fill(item, amount);
    }

    //value == money
    public override bool Enable(int value = 0)
    {
        m_consumable = value >= m_item.price;
        if (m_consumable)
        {
            m_frameSelected.SetActive(false);
            m_image.color = Color.white;
            m_amountUI.color = m_colorTextNormal;
        }
        else
        {
            m_frameSelected.SetActive(false);
            m_image.color = m_colorDisabled;
            m_amountUI.color = m_colorTextNormal;
        }
        return m_consumable;
    }

    public override void OnClick()
    {
        Select();
        base.OnClick();
    }

    public void Select()
    {
        m_frameSelected.SetActive(true);
        m_amountUI.color = m_colorTextSelected;
    }

    public void Unselect()
    {
        if (m_consumable)
        {
            m_frameSelected.SetActive(false);
            m_image.color = Color.white;
            m_amountUI.color = m_colorTextNormal;
        }
        else
        {
            m_frameSelected.SetActive(false);
            m_image.color = m_colorDisabled;
            m_amountUI.color = m_colorTextNormal;
        }
    }
}
