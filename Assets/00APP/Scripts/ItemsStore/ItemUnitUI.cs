using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUnitUI : MonoBehaviour {
    [HideInInspector]
    public InventoryUnitsBankItem m_item;
    [HideInInspector]
    public GamepediaDefenderBankItem m_description;
    public Text m_coinsUI;
    public Text m_unlockCoinsUI;
    public Image m_image;

    public GameObject m_frameSelected;
    public Color m_colorDisabled;
    public Color m_colorTextNormal;
    public Color m_colorTextSelected;
    public bool m_consumable;

    public virtual void Fill(InventoryUnitsBankItem item, GamepediaDefenderBankItem description)
    {
        m_item = item;
        m_description = description;
        m_coinsUI.text = item.coins.ToString();
        m_image.sprite = item.sprite;
        m_unlockCoinsUI.text = item.unlockCoins.ToString();
    }

    public bool Enable(int value = 0)
    {
        m_consumable = value >= m_item.unlockCoins;
        if (m_consumable)
        {
            m_frameSelected.SetActive(false);
            m_image.color = Color.white;
            m_coinsUI.color = m_colorTextNormal;
        }
        else
        {
            m_frameSelected.SetActive(false);
            m_image.color = m_colorDisabled;
            m_coinsUI.color = m_colorTextNormal;
        }
        return m_consumable;
    }

    public void OnClick()
    {
        Select();
        InventoryItemsEvents.DispatchItemUnitSelected(this);
    }

    public void Select()
    {
        m_frameSelected.SetActive(true);
        m_coinsUI.color = m_colorTextSelected;
    }

    public void Unselect()
    {
        if (m_consumable)
        {
            m_frameSelected.SetActive(false);
            m_image.color = Color.white;
            m_coinsUI.color = m_colorTextNormal;
        }
        else
        {
            m_frameSelected.SetActive(false);
            m_image.color = m_colorDisabled;
            m_coinsUI.color = m_colorTextNormal;
        }
    }
}
