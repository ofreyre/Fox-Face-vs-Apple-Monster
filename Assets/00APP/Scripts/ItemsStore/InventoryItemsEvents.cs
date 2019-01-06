using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsEvents : MonoBehaviour {

    public static InventoryItemsEvents instance;

    public delegate void DelegateItemSelected(ItemUI item);
    public DelegateItemSelected ItemSelected;

    public delegate void DelegateItemConsumed(INVENTORYITEM_ITEM_CATEGORY category, float amount);
    public DelegateItemConsumed ItemConsumed;

    public delegate void DelegateItemUnitSelected(ItemUnitUI item);
    public DelegateItemUnitSelected ItemUnitSelected;

    public delegate void DelegateItemItemUnitRemoved();
    public DelegateItemItemUnitRemoved ItemUnitRemoved;

    void Awake()
    {
        instance = this;
    }

    public static void DispatchItemSelected(ItemUI item)
    {
        if (instance.ItemSelected != null)
        {
            instance.ItemSelected(item);
        }
    }

    public static void DispatchItemConsumed(INVENTORYITEM_ITEM_CATEGORY category, float amount)
    {
        if (instance.ItemConsumed != null)
        {
            instance.ItemConsumed(category, amount);
        }
    }

    public static void DispatchItemUnitSelected(ItemUnitUI item)
    {
        if (instance.ItemUnitSelected != null)
        {
            instance.ItemUnitSelected(item);
        }
    }

    public static void DispatchItemUnitRemoved()
    {
        if (instance.ItemUnitRemoved != null)
        {
            instance.ItemUnitRemoved();
        }
    }

}
