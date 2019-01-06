using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInventory : MonoBehaviour
{
    public DBcatalog m_catalog;
    public Transform m_list;
    public Transform m_listCountainer;
    public GameObject m_inventory;
    public GameObject m_itemPrefab;
    int m_itemsC;
    DBinventoryItem local_item;
    ItemUI local_itemUI;

    // Use this for initialization
    public void Init () {
        int[] itemsAmount = DBmanager.Inventory.items;
        int itemAmount;
        
        for (int i = 0, n = itemsAmount.Length; i < n; i++) {
            itemAmount = itemsAmount[i];
            if (itemAmount > 0) {
                local_item = m_catalog.GetItem((INVENTORYITEM_TYPE)i);
                if (local_item.visibleInInventory)
                {
                    local_itemUI = Instantiate(m_itemPrefab).GetComponent<ItemUI>();
                    local_itemUI.Fill(local_item, itemAmount);
                    local_itemUI.Enable(0);
                    local_itemUI.transform.SetParent(m_list);
                    local_itemUI.transform.localScale = Vector3.one;
                    //local_itemUI.Fit();
                    local_itemUI.transform.localScale = new Vector3(1f, 1f, 1f);
                    m_itemsC++;
                }
            }
        }
        if (m_itemsC == 0)
        {
            Destroy(m_inventory);
            Destroy(m_list.gameObject);
            InventoryItemsEvents.DispatchItemSelected(null);
        }
        else
        {
            m_list.SetParent(m_listCountainer);
            InventoryItemsEvents.instance.ItemSelected += Add;
        }

    }

    public void Add(ItemUI item)
    {
        local_item = item.m_item;
        int amount = DBmanager.AddItems(local_item.type, -1);
        if (amount > 0)
        {
            item.Amount = amount;
        }
        else
        {
            Destroy(item.gameObject);
            m_itemsC--;
        }
        if (m_itemsC < 1)
        {
            InventoryItemsEvents.instance.ItemSelected -= Add;
            InventoryItemsEvents.DispatchItemSelected(null);
        }
        InventoryItemsEvents.DispatchItemConsumed(local_item.category, local_item.amount);
    }

    public void Display(bool display) {
        if (!display)
        {
            //m_inventory.SetActive(false);
            Destroy(m_inventory);
        }
        else
        {
            if (m_itemsC < 1)
            {
                InventoryItemsEvents.instance.ItemSelected -= Add;
                InventoryItemsEvents.DispatchItemSelected(null);
                //m_inventory.SetActive(false);
                Destroy(m_inventory);
            }
            else
            {
                m_inventory.SetActive(true);
            }
        }
    }
}
