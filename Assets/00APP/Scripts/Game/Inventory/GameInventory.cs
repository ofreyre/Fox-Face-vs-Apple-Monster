using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameInventory : MonoBehaviour
{
    public static GameInventory instance;

    public InventoryUnitsBank m_unitsBank;
    public LevelInventory m_inventory;
    public Transform m_container;
    public float m_rowHeight = 80;
    public Text text;
    [HideInInspector]
    public UnitBankItemClick[] m_units;
    GameObject[] m_selection;
    int[] m_prices;
    public UNITTYPE[] m_types;
    bool[] m_recovering;
    public int m_coins;
    Dictionary<UNITTYPE, UnitBankItemClick> m_unitsByType;
    public UNITTYPE m_inventoryType = UNITTYPE.none;

    float m_recoveryUpgrade;

    // Use this for initialization
    public void Init ()
    {
        instance = this;
        ItemsEvents.instance.UnitClick += OnUnitClick;
        GameEvents.instance.BabyPacmanCollected += OnBabyPacmanCollected;
        GameEvents.instance.UnitAdded += OnUnitAdded;
        GameEvents.instance.RemoveClicked += OnRemoveClicked;
        GameEvents.instance.UnitItemRecovered += OnUnitItemRecovered;

        int n = m_inventory.m_units.Length;
        InventoryUnitsBankItem[] units = new InventoryUnitsBankItem[n];
        for (int i = 0; i < n; i++)
        {
            units[i] = m_unitsBank.UNITTYPE_2_Item(m_inventory.m_units[i]);
        }
        Array.Sort(units, delegate (InventoryUnitsBankItem g1, InventoryUnitsBankItem g2)
        {
            return g1.coins.CompareTo(g2.coins);
        });

        m_units = new UnitBankItemClick[n];
        m_prices = new int[n];
        m_types = new UNITTYPE[n];
        m_selection = new GameObject[n];
        m_recovering = new bool[n];
        Transform ts;
        InventoryUnitsBankItem item;
        
        m_unitsByType = new Dictionary<UNITTYPE, UnitBankItemClick>(UnitType.UnitTypeComparer);
        for (int i = 0; i < n; i++)
        {
            item = units[i];
            ts = m_unitsBank.GetButton(item);
            ts.SetParent(m_container, false);
            ts.localScale = Vector3.one;
            ts.localPosition = new Vector3(0, -i * m_rowHeight, 0);
            m_types[i] = item.type;
            m_prices[i] = item.coins;
            m_units[i] = ts.GetComponent<UnitBankItemClick>();
            m_recovering[i] = false;
            if (m_prices[i] <= m_coins)
            {
                m_units[i].Enable();
            }
            else
            {
                m_units[i].Disable();
            }
            m_selection[i] = ts.Find("btnSelected").gameObject;
            m_unitsByType[item.type] = m_units[i];
        }
        coins = m_coins;
    }

    public void ApplyUpgrades(float recovery)
    {
        m_recoveryUpgrade = recovery;
    }
	
	public int coins
    {
        get { return m_coins; }
        set {
            m_coins = value;
            for (int i = 0, n = m_prices.Length; i < n; i++)
            {
                if (m_coins >= m_prices[i])
                {
                    if (!m_recovering[i])
                    {
                        m_units[i].Enable();
                    }
                }
                else
                {
                    m_units[i].Disable();
                }
            }
            text.text = m_coins.ToString();
        }
    }

    void OnUnitClick(UNITTYPE type)
    {
        GameEvents.DispatchInventoryClicked();
        if (m_inventoryType != type)
        {
            if (m_inventoryType != UNITTYPE.none)
            {
                m_selection[Array.IndexOf(m_types, m_inventoryType)].SetActive(false);
            }
            m_inventoryType = type;
            m_selection[Array.IndexOf(m_types, m_inventoryType)].SetActive(true);
        }
        else
        {
            m_selection[Array.IndexOf(m_types, m_inventoryType)].SetActive(false);
            m_inventoryType = UNITTYPE.none;
        }
    }

    void OnBabyPacmanCollected()
    {
        coins += 1;
    }

    void OnUnitAdded(int i, int j)
    {
        int k = Array.IndexOf(m_types, m_inventoryType);
        m_recovering[k] = true;
        //m_selection[Array.IndexOf(m_types, m_inventoryType)].SetActive(false);
        m_unitsByType[m_inventoryType].Recover(m_unitsBank.UNITTYPE_2_Item(m_inventoryType).recover - m_recoveryUpgrade);
        coins -= m_prices[k];
        m_inventoryType = UNITTYPE.none;
    }

    void OnRemoveClicked()
    {
        if (m_inventoryType != UNITTYPE.none)
        {
            m_selection[Array.IndexOf(m_types, m_inventoryType)].SetActive(false);
            m_inventoryType = UNITTYPE.none;
        }
    }

    void OnUnitItemRecovered(UNITTYPE type)
    {
        int i = Array.IndexOf(m_types, type);
        m_recovering[i] = false;
        if (coins >= m_prices[i])
        {
            m_units[i].Enable();
        }
    }
}
