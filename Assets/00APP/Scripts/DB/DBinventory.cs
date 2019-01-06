using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DBinventory {
    public int coins;
    public int[] items;

    public DBinventory(int coins) {
        this.coins = coins;
        items = new int[UtilsEnum.EnumCount<INVENTORYITEM_TYPE>()];
    }

    public int Coins {
        get {
            return coins;
        }

        set {
            coins = Mathf.Max(0, value);
        }
    }

    public int AddItems(INVENTORYITEM_TYPE type, int amount) {
        int i = (int)type;
        items[i] = Mathf.Max(0, items[i] + amount);
        return items[i];
    }

    public int GetItems(INVENTORYITEM_TYPE type) {
        return items[(int)type];
    }
}
