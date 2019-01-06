using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepediaEvents : MonoBehaviour
{
    public static GamepediaEvents instance;

    public delegate void DelegateAttackerSelected(ATTACKERTYPE type);
    public event DelegateAttackerSelected AttackerSelected;

    public delegate void DelegateUnitSelected(UNITTYPE type);
    public event DelegateUnitSelected UnitSelected;

    void Awake()
    {
        instance = this;
    }

    public static void DispatchAttackerSelected(ATTACKERTYPE type)
    {
        if (instance.AttackerSelected != null)
        {
            instance.AttackerSelected(type);
        }
    }

    public static void DispatchUnitSelected(UNITTYPE type)
    {
        if (instance.UnitSelected != null)
        {
            instance.UnitSelected(type);
        }
    }
}
