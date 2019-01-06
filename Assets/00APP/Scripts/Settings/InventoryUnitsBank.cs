using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUnitsBank : ScriptableObject
{
    public InventoryUnitsBankItem[] items;
    public GameObject btnUnit;
    public GameObject emptySlot;
    public GameObject videoSlot;
    public GameObject lockIco;
    public Vector3 lockIcoPos;
    Transform ts0, ts1;

    public InventoryUnitsBankItem UNITTYPE_2_Item(UNITTYPE type)
    {
        InventoryUnitsBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item;
            }
        }
        return InventoryUnitsBankItem.NONE;
    }

    public InventoryUnitsBankItem GetItemAvailable(int order)
    {
        InventoryUnitsBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.order <= order)
            {
                if (item.available && item.order == order)
                {
                    return item;
                }
            }
            else
            {
                break;
            }
        }
        return InventoryUnitsBankItem.NONE;
    }

    public Transform GetButton(UNITTYPE type, LevelUnitsBank bank = null)
    {
        return GetButton(UNITTYPE_2_Item(type), bank);
    }

    public Transform GetButton(UNITTYPE type, bool interactable)
    {
        return GetButton(UNITTYPE_2_Item(type), interactable);
    }

    public Transform GetButton(InventoryUnitsBankItem item, LevelUnitsBank bank = null)
    {
        return GetButton(item, bank != null ? bank.IsItemAvailable(item.type) && !bank.IsUnitTypeSelected(item.type):true);
    }

    public Transform GetButton(InventoryUnitsBankItem item, bool interactable)
    {
        ts0 = Instantiate(btnUnit).transform;
        ts0.GetComponent<UnitType>().type = item.type;
        ts1 = ts0.Find("btn");
        ts1.GetComponent<Image>().sprite = item.sprite;
        ts1.GetComponent<Button>().interactable = interactable;
        ts0.Find("txtPrice").GetComponent<Text>().text = item.coins.ToString();
        return ts0;
    }

    public void SorteByOrder()
    {
        Array.Sort(items, delegate (InventoryUnitsBankItem item1, InventoryUnitsBankItem item2)
        {
            return item1.order.CompareTo(item2.order);
        });
    }
}
