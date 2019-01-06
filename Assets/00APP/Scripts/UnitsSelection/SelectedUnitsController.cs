using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectedUnitsController : MonoBehaviour
{
    //public ScriptabletGameObjectList m_bank;
    public LevelsSettings m_settings;
    public InventoryUnitsBank m_unitsBank;
    public LevelInventory m_gameInventory;
    public GlobalFlow m_flow;
    public int m_unitsMaxLimit = 9;
    public int m_unitsMaxCount = 8;
    public int m_unitsCount = 7;
    public int m_fixedCount = 3;
    public Vector2 m_leftTop = new Vector2(10, -10);
    public float m_rowHeight = 80;
    public Transform m_container;
    public UnitSelectionPlay m_gamePlayer;
    //LevelUnitsBank m_levelBank;
    GameObject[] m_units;
    int m_unitsTotal;
    int m_currentSlot;
    UNITTYPE[] m_selected;

    public void Init()
    {
        ItemsEvents.instance.UnitSelected += OnUnitSelected;
        ItemsEvents.instance.SelectedItemClick += OnSelectedItemClick;

        //m_levelBank = m_settings.settings[m_flow.AbsoluteLevel].unitsBank;
        
        m_units = new GameObject[m_unitsMaxLimit];

        GameObject gobj, lockIco;
        int i = 0;
        Button btn;
        ColorBlock colors = ColorBlock.defaultColorBlock;
        colors.disabledColor = Color.white;
        DBunits dbUnits = DBmanager.Units;
        int n = dbUnits.Count;

        if (n <= m_unitsCount)
        {
            for (i = 0; i < n; i++)
            {
                gobj = Instantiate(m_unitsBank.emptySlot);
                gobj.transform.SetParent(m_container, false);
                gobj.transform.localScale = Vector3.one;
                gobj.transform.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * i, 0);

                gobj = AddUnit(i, dbUnits.items[i]);
                btn = gobj.GetComponent<Button>();
                //btn.interactable = false;
                btn.colors = colors;
                //lockIco = Instantiate(m_unitsBank.lockIco);
                //lockIco.transform.parent = gobj.transform;
                //lockIco.transform.localScale = Vector3.one;
                //lockIco.transform.localPosition = m_unitsBank.lockIcoPos;
                ItemsEvents.DispatchSelectedItemAddedByType(dbUnits.items[i]);
            }
            m_unitsTotal = n;
        }
        else
        {
            if (m_gameInventory.absoluteLevel != m_flow.AbsoluteLevel || m_gameInventory.m_units == null || m_gameInventory.m_units.Length == 0)
            {
                UnitsFixed unitsFixed = dbUnits.GetFixed(m_fixedCount, m_unitsCount);
                for (i=0; i < m_fixedCount; i++)
                {
                    gobj = AddUnit(i, unitsFixed.fixedUnits[i]);
                    btn = gobj.GetComponent<Button>();
                    btn.interactable = false;
                    btn.colors = colors;
                    lockIco = Instantiate(m_unitsBank.lockIco);
                    lockIco.transform.SetParent(gobj.transform, false);
                    lockIco.transform.localScale = Vector3.one;
                    lockIco.transform.localPosition = m_unitsBank.lockIcoPos;
                    ItemsEvents.DispatchSelectedItemAddedByType(unitsFixed.fixedUnits[i]);
                }
                for (i = m_fixedCount; i < m_unitsCount; i++)
                {
                    AddBlank(i);
                    /*if (unitTypes != null && i < n)
                    {
                        type = unitTypes[i];
                        if (type != UNITTYPE.none)
                        {
                            AddUnit(i, type);
                            ItemsEvents.DispatchSelectedItemAddedByType(type);
                        }
                    }*/
                }

            }
            else
            {
                n = m_gameInventory.m_units.Length;
                for (i=0 ; i < n; i++)
                {
                    if (i >= m_fixedCount)
                    {
                        gobj = Instantiate(m_unitsBank.emptySlot);
                        gobj.transform.SetParent(m_container, false);
                        gobj.transform.localScale = Vector3.one;
                        gobj.transform.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * i, 0);
                    }
                    gobj = AddUnit(i, m_gameInventory.m_units[i]);
                    btn = gobj.GetComponent<Button>();
                    btn.colors = colors;
                    ItemsEvents.DispatchSelectedItemAddedByType(m_gameInventory.m_units[i]);
                    if (i < m_fixedCount)
                    {
                        lockIco = Instantiate(m_unitsBank.lockIco);
                        lockIco.transform.SetParent(gobj.transform, false);
                        lockIco.transform.localScale = Vector3.one;
                        lockIco.transform.localPosition = m_unitsBank.lockIcoPos;
                        btn.interactable = false;
                    }
                }

                m_unitsCount = m_unitsCount > n ? m_unitsCount : n;

                for (i = n; i < m_unitsCount; i++)
                {
                    AddBlank(i);
                }

                //unitTypes = m_gameInventory.m_units;
                //n = unitTypes.Length;
            }


            /*UNITTYPE type;
            for (i = m_unitsMinCount; i < m_unitsCount; i++)
            {
                gobj = Instantiate(m_unitsBank.emptySlot);
                gobj.transform.parent = m_container;
                gobj.transform.localScale = Vector3.one;
                gobj.transform.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * i, 0);
                if (unitTypes != null && i < n)
                {
                    type = unitTypes[i];
                    if (type != UNITTYPE.none)
                    {
                        AddUnit(i, type);
                        ItemsEvents.DispatchSelectedItemAddedByType(type);
                    }
                }
            }*/

            if (m_unitsCount == m_unitsMaxCount && m_unitsMaxCount < m_unitsMaxLimit)
            {
                m_unitsMaxCount++;
            }
            for (i = m_unitsCount; i < m_unitsMaxCount; i++)
            {
                AddVideo(i);
            }

            m_unitsTotal = m_unitsCount;
        }
    }

    GameObject AddUnit(int i, Transform ts)
    {
        Button button = ts.Find("btn").GetComponent<Button>();
        DestroyImmediate(button);
        Destroy(ts.GetComponent<UnitBankItemClick>());

        button = ts.gameObject.AddComponent<Button>();
        //button.onClick.RemoveListener(gobj.GetComponent<UnitBankItemClick>().OnClick);

        SelectedItemClick click = ts.gameObject.AddComponent<SelectedItemClick>();
        button.onClick.AddListener(click.OnClick);

        ts.SetParent(m_container, false);
        ts.localScale = Vector3.one;
        ts.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * m_currentSlot, 0);
        m_units[m_currentSlot] = ts.gameObject;
        m_currentSlot++;
        if (m_currentSlot == m_unitsCount && m_unitsCount == m_unitsMaxCount && m_unitsMaxCount < m_unitsMaxLimit)
        {
            m_unitsMaxCount++;
            Debug.Log("AddUnit " + m_currentSlot + " " + m_unitsCount + " " + m_unitsTotal);
            AddVideo(m_currentSlot);
        }
        return ts.gameObject;
    }

    GameObject AddUnit(int i, UNITTYPE type)
    {
        Transform ts = m_unitsBank.GetButton(type);
        return AddUnit(i, ts);
    }

    void AddBlank(int i)
    {
        GameObject gobj = Instantiate(m_unitsBank.emptySlot);
        gobj.transform.SetParent(m_container, false);
        gobj.transform.localScale = Vector3.one;
        gobj.transform.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * i, 0);
    }

    void AddVideo(int i)
    {
        GameObject gobj = Instantiate(m_unitsBank.videoSlot);
        gobj.transform.SetParent(m_container, false);
        gobj.transform.localScale = Vector3.one;
        gobj.transform.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * i, 0);
        gobj.GetComponent<btnAddSlot>().Init(i, this);
    }

    void OnUnitSelected(UNITTYPE type)
    {
        if (m_currentSlot < m_unitsTotal)
        {
            GameObject gobj = m_units[m_currentSlot];
            Destroy(gobj);
            AddUnit(m_currentSlot, type);
            ItemsEvents.DispatchSelectedItemAdded(type, true);
        }
        else
        {
            ItemsEvents.DispatchSelectedItemAdded(type, false);
        }
    }

    void OnSelectedItemClick(GameObject item)
    {
        int i = Item_2_index(item);
        UnitsDown(i);
        ItemsEvents.DispatchSelectedItemSelected(item);
        Destroy(item);
    }

    void UnitsDown(int slotI)
    {
        GameObject gobj;
        for (int i = slotI + 1; i < m_unitsTotal; i++)
        {
            gobj = m_units[i];
            if (gobj != null)
            {
                m_units[i - 1] = gobj;
                m_units[i] = null;
                gobj.transform.localPosition = new Vector3(m_leftTop.x, m_leftTop.y - m_rowHeight * (i-1), 0);
            }
        }
        m_currentSlot--;
    }

    int Item_2_index(GameObject item)
    {
        return Array.IndexOf(m_units, item);
    }

    private void OnDestroy()
    {
        UpdateInventoryUnits();
    }

    void UpdateInventoryUnits()
    {
        if (m_units.Length > 0)
        {
            UNITTYPE[] units = new UNITTYPE[m_currentSlot];
            for (int i = 0; i < m_currentSlot; i++)
            {
                units[i] = m_units[i].GetComponent<UnitType>().type;
            }
            m_gameInventory.units = units;
        }
        else
        {
            m_gameInventory.units = null;
        }
        m_gameInventory.slotsCount = m_unitsTotal;
    }

    public void Play(string scene)
    {
        if (m_currentSlot < m_unitsTotal)
        {
            EventManagerMessages.instance.DispatchMessage("Please fill all unit slots.");
        }
        else
        {
            m_gamePlayer.Play();
        }

    }

    public void AddSlot(int i)
    {
        m_unitsCount++;
        m_unitsTotal = m_unitsCount;
        Debug.Log("AddSlot "+m_currentSlot + " " + m_unitsCount + " " + m_unitsTotal);
        AddBlank(m_unitsCount - 1);
    }
}
