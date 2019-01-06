using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesEvents : MonoBehaviour {

    public static UpgradesEvents instance;
    public delegate void DelegateUpgradeSelected(INVENTORYITEM_CATEGORY category, int index, bool add);
    public DelegateUpgradeSelected UpgradeSelected;

    void Awake()
    {
        instance = this;
    }

    public void DispatchUpgradeSelected(INVENTORYITEM_CATEGORY category, int index, bool add)
    {
        if (UpgradeSelected != null)
        {
            UpgradeSelected(category, index, add);
        }
    }
}
