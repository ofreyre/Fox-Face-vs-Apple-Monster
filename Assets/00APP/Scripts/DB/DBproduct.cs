using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum PRODUCT_TYPE
{
    coins100,
    coins250,
    coins600,
    coins1300,
    coins2800,
    coins6000,
    coins15000,
    coins40000,
    coins100000,
    coins500000
}

public class DBproduct
{
    public PRODUCT_TYPE type;
    public string name;
    public string description;
    public GameObject m_inventoryIcon;
    public int price;
    public int coins;
}
