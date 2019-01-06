using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepediaFillItems : MonoBehaviour {
    public GamepediaBank m_bank;
    public InventoryUnitsBank m_unitsBank;
    public Transform m_scroll;
    public Transform m_attackersContainer;
    public Transform m_unitsContainer;
    public GameObject m_itemPrefabAttacker;
    public GameObject m_itemPrefabUnit;

    void Start () {
        FIllAttackers();
        FillUnits();
        DisplayUnits();
    }

    void Clear()
    {
        foreach (Transform ts in m_attackersContainer)
        {
            Destroy(ts.gameObject);
        }

        foreach (Transform ts in m_unitsContainer)
        {
            Destroy(ts.gameObject);
        }
    }
    
    void FIllAttackers()
    {

        GameObject gobj;
        GamepediaAttackerBankItem[] attackers = m_bank.attackers;
        GamepediaAttackerBankItem item;
        for (int i = 0, n = attackers.Length; i < n; i++)
        {
            item = attackers[i];
            if (item.display && item.prefab != null)
            {
                gobj = Instantiate(m_itemPrefabAttacker);
                gobj.GetComponent<btnGamepediaAttacker>().Fill(item);
                gobj.transform.SetParent(m_attackersContainer);
                gobj.transform.localScale = Vector3.one;
            }
        }
        m_attackersContainer.SetParent(m_scroll);
    }

    void FillUnits()
    {
        GameObject gobj;
        InventoryUnitsBankItem[] items = m_unitsBank.items;
        InventoryUnitsBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.available)
            {
                gobj = Instantiate(m_itemPrefabUnit);
                gobj.GetComponent<btnGamepediaUnit>().Fill(item);
                gobj.transform.SetParent(m_unitsContainer);
                gobj.transform.localScale = Vector3.one;
            }
        }
        m_unitsContainer.SetParent(m_scroll);
    }

    public void DisplayUnits()
    {
        GamepediaEvents.DispatchUnitSelected(UNITTYPE.none);
        m_unitsContainer.gameObject.SetActive(true);
        m_attackersContainer.gameObject.SetActive(false);
        m_scroll.GetComponent<ScrollRect>().content = m_unitsContainer.GetComponent<RectTransform>();
    }

    public void DisplayAttackers()
    {
        GamepediaEvents.DispatchAttackerSelected(ATTACKERTYPE.none);
        m_unitsContainer.gameObject.SetActive(false);
        m_attackersContainer.gameObject.SetActive(true);
        m_scroll.GetComponent<ScrollRect>().content = m_attackersContainer.GetComponent<RectTransform>();
    }
}
