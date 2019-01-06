using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsBankController : MonoBehaviour
{
    public LevelsSettings m_settings;
    public InventoryUnitsBank m_unitsBank;
    public GlobalFlow m_flow;
    public Transform m_grid;
    public Transform m_scroll;
    //LevelUnitsBank m_levelBank;

    public void Init()
    {
        ItemsEvents.instance.SelectedItemAddedByType += OnSelectedItemAddedByType;
        ItemsEvents.instance.UnitClick += OnUnitClick;
        ItemsEvents.instance.SelectedItemAdded += OnSelectedItemAdded;
        ItemsEvents.instance.SelectedItemSelected += OnSelectedItemSelected;

        //m_levelBank = m_settings.settings[m_flow.AbsoluteLevel].unitsBank;
        m_unitsBank.SorteByOrder();
        InventoryUnitsBankItem[] items = m_unitsBank.items;
        Transform ts;
        InventoryUnitsBankItem item;
        DBunits dbUnits = DBmanager.Units;

        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (dbUnits.Contains(item.type))
            {
                ts = m_unitsBank.GetButton(item, true);
                ts.SetParent(m_grid, false);
                ts.localScale = Vector3.one;
            }
        }
        m_grid.SetParent(m_scroll, false);
    }

    void OnSelectedItemAddedByType(UNITTYPE type)
    {
        UNITTYPE_2_Transform(type).Find("btn").GetComponent<Button>().interactable = false;
    }

    void OnUnitClick(UNITTYPE type)
    {
        ItemsEvents.DispatchUnitSelected(type);
    }

    void OnSelectedItemAdded(UNITTYPE type, bool success)
    {
        UNITTYPE_2_Transform(type).Find("btn").GetComponent<Button>().interactable = !success;
    }

    void OnSelectedItemSelected(GameObject item)
    {
        Transform ts = UNITTYPE_2_Transform(item.GetComponent<UnitType>().type);
        if (ts != null)
        {
            ts.Find("btn").GetComponent<Button>().interactable = true;
        }
    }

    Transform UNITTYPE_2_Transform(UNITTYPE type)
    {
        foreach (Transform ts in m_grid)
        {
            if (ts.GetComponent<UnitType>().type == type)
            {
                return ts;
            }
        }
        return null;
    }
}
