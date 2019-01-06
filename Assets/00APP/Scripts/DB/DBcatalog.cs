using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBcatalog : ScriptableObject
{
    public DBinventoryItem[] items;

    public DBinventoryItem GetItem(INVENTORYITEM_TYPE type) {
        DBinventoryItem item;
        for (int i = 0, n = items.Length; i < n; i++) {
            item = items[i];
            if (item.type == type) {
                return item;
            }
        }
        return null;
    }
}
