using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoreList : MonoBehaviour {
    public DBcatalog m_catalog;
    public InventoryUnitsBank m_unitsBank;
    public GamepediaBank m_gamepedia;
    public GameObject m_itemPrefab;
    public GameObject m_itemUnitPrefab;
    public Transform m_container;
    StoreItemUI[] m_items;
    List<ItemUnitUI> m_units;

    void Start()
    {
        DBinventory inventory = DBmanager.Inventory;
        int money = inventory.coins;

        //Item Units
        List<UNITTYPE> unlockedUnits = DBmanager.Units.items;
        m_unitsBank.SorteByOrder();
        InventoryUnitsBankItem[] units = m_unitsBank.items;
        InventoryUnitsBankItem unitItem;
        ItemUnitUI m_unit;
        m_units = new List<ItemUnitUI>();
        //int unlockedLevelMax = DBmanager.UnlockedLevelMax;
        int lastWonLevel = DBmanager.LastWonLevel;
        for (int i = 0, n = units.Length; i < n; i++)
        {
            unitItem = units[i];
            if (unitItem.order <= lastWonLevel)
            {
                if (unitItem.available && !unlockedUnits.Contains(unitItem.type))
                {
                    m_unit = Instantiate(m_itemUnitPrefab).GetComponent<ItemUnitUI>();
                    m_unit.Enable(money);
                    m_unit.transform.SetParent(transform);
                    m_unit.transform.localScale = Vector3.one;
                    m_unit.Fill(unitItem, m_gamepedia.GetDefender(unitItem.type));
                    m_units.Add(m_unit);
                }
            }
            else
            {
                break;
            }
        }

        //Items
        DBinventoryItem[] items = m_catalog.items;
        Array.Sort(items, DBinventoryItem.CompareDBinventoryItem);
        m_items = new StoreItemUI[items.Length];
        DBinventoryItem item;
        StoreItemUI storeItem;
        for (int i = 0, n = items.Length; i < n; i++) {
            item = items[i];
            if (item.visibleInInventory)
            {
                storeItem = Instantiate(m_itemPrefab).GetComponent<StoreItemUI>();
                storeItem.Fill(item, inventory.GetItems(item.type));
                storeItem.Enable(money);
                storeItem.transform.SetParent(transform);
                storeItem.transform.localScale = Vector3.one;
                //storeItem.Fit();
                m_items[i] = storeItem;
            }
        }
        transform.SetParent(m_container);
    }

    public int AddAmount(StoreItemUI item, int amount)
    {
        item.Amount += amount;
        return item.Amount;
    }

    public void RemoveUnit(ItemUnitUI item)
    {
        if (m_units.Contains(item))
        {
            m_units.Remove(item);
        }
    }

    public void UpdateItemsEnabled(int money)
    {
        ItemUnitUI _unit;
        for (int i = m_units.Count - 1; i > -1; i--)
        {
            _unit = m_units[i];
            if (_unit != null)
            {
                _unit.Enable(money);
            }
        }

        StoreItemUI _item;
        for (int i = m_items.Length - 1; i > -1; i--)
        {
            _item = m_items[i];
            if (_item != null)
            {
                _item.Enable(money);
            }
        }
    }
}