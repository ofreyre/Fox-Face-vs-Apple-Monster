using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEvents : MonoBehaviour
{
    public static ItemsEvents instance;

    public delegate void DelegateSelectedItemAddedByType(UNITTYPE type);
    public event DelegateSelectedItemAddedByType SelectedItemAddedByType;

    public delegate void DelegateUnitBankItemClick(UNITTYPE type);
    public event DelegateUnitBankItemClick UnitClick;

    public delegate void DelegateUnitBankItemSelected(UNITTYPE type);
    public event DelegateUnitBankItemSelected UnitSelected;

    public delegate void DelegateSelectedItemAdded(UNITTYPE type, bool success);
    public event DelegateSelectedItemAdded SelectedItemAdded;

    public delegate void DelegateSelectedItemClick(GameObject item);
    public event DelegateSelectedItemClick SelectedItemClick;

    public delegate void DelegateSelectedItemSelected(GameObject item);
    public event DelegateSelectedItemSelected SelectedItemSelected;

    void Awake()
    {
        instance = this;
    }

    public static void DispatchSelectedItemAddedByType(UNITTYPE type)
    {

        if (instance.SelectedItemAddedByType != null)
        {
            instance.SelectedItemAddedByType(type);
        }
    }

    public static void DispatchUnitClick(UNITTYPE type)
    {
        if (instance.UnitClick != null)
        {
            instance.UnitClick(type);
        }
    }

    public static void DispatchUnitSelected(UNITTYPE type)
    {
        if (instance.UnitSelected != null)
        {
            instance.UnitSelected(type);
        }
    }

    public static void DispatchSelectedItemAdded(UNITTYPE type, bool success)
    {
        if (instance.SelectedItemAdded != null)
        {
            instance.SelectedItemAdded(type, success);
        }
    }

    public static void DispatchSelectedItemClick(GameObject item)
    {
        if (instance.SelectedItemClick != null)
        {
            instance.SelectedItemClick(item);
        }
    }

    public static void DispatchSelectedItemSelected(GameObject item)
    {
        if (instance.SelectedItemSelected != null)
        {
            instance.SelectedItemSelected(item);
        }
    }
}
