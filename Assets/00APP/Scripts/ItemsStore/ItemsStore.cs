using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManagement;

public class ItemsStore : MonoBehaviour {

    public EventManagerMessages m_eventManager;
    public Text m_moneyUI;
    public StoreList m_list;
    public Button m_btnAdd;
    public string m_consumeSFX;
    public int m_consumeSFX_key;
    ItemUI m_itemUI;
    ItemUnitUI m_itemUnitUI;

    // Use this for initialization
    void Start () {
        m_consumeSFX_key = m_consumeSFX.GetHashCode();
        m_moneyUI.text = DBmanager.Coins.ToString();
        InventoryItemsEvents.instance.ItemSelected += Selected;
        InventoryItemsEvents.instance.ItemUnitSelected += Selected;
        m_eventManager.Int += UpdateCoinsView;
    }

    void Selected(ItemUI itemUI)
    {
        if (m_itemUI != itemUI)
        {
            if (m_itemUI != null)
            {
                ((StoreItemUI)m_itemUI).Unselect();
            }
            m_itemUI = itemUI;
            m_btnAdd.interactable = DBmanager.Coins >= m_itemUI.m_item.price;
            if (m_itemUnitUI != null)
            {
                m_itemUnitUI.Unselect();
                m_itemUnitUI = null;
            }
        }
    }

    void Select()
    {
        
    }

    void Selected(ItemUnitUI itemUI)
    {
        if (m_itemUnitUI != itemUI)
        {
            if (m_itemUnitUI != null)
            {
                m_itemUnitUI.Unselect();
            }
            m_itemUnitUI = itemUI;
            m_btnAdd.interactable = DBmanager.Coins >= m_itemUnitUI.m_item.unlockCoins;
            if (m_itemUI != null)
            {
                ((StoreItemUI)m_itemUI).Unselect();
                m_itemUI = null;
            }
        }
    }

    public void Purchase() {
        int coins = DBmanager.Coins;

        if (m_itemUI != null)
        {
            DBinventoryItem item = m_itemUI.m_item;
            if (coins >= item.price)
            {
                AudioManager.instance.Play(m_consumeSFX_key);
                DBmanager.AddItems(item.type, 1);
                AddCoins(-item.price);
                m_list.AddAmount((StoreItemUI)m_itemUI, 1);
                ((StoreItemUI)m_itemUI).Select();
            }
        }
        else if (m_itemUnitUI != null)
        {
            if (coins >= m_itemUnitUI.m_item.unlockCoins)
            {
                AudioManager.instance.Play(m_consumeSFX_key);
                DBmanager.AddUnit(m_itemUnitUI.m_item.type);
                coins = -m_itemUnitUI.m_item.unlockCoins;
                m_list.RemoveUnit(m_itemUnitUI);
                Destroy(m_itemUnitUI.gameObject);
                AddCoins(coins);
                m_itemUnitUI = null;
            }
        }
    }

    public void AddCoins(int amount)
    {
        DBmanager.Coins += amount;
        UpdateCoinsView();
    }

    void UpdateCoinsView(int amount = 0)
    {
        int coins = DBmanager.Coins;
        m_moneyUI.text = coins.ToString();
        m_list.UpdateItemsEnabled(coins);

        if (m_itemUI != null)
        {
            ((StoreItemUI)m_itemUI).Select();
            m_btnAdd.interactable = DBmanager.Coins >= m_itemUI.m_item.price;
        }
        else if (m_itemUnitUI != null)
        {
            m_btnAdd.interactable = DBmanager.Coins >= m_itemUnitUI.m_item.unlockCoins;
            m_itemUnitUI.Select();
        }
    }
}
