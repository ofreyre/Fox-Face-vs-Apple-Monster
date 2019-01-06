using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBproductsCatalog : ScriptableObject
{
    public DBproduct[] items;

    public void Generate()
    {
        PRODUCT_TYPE[] types = UtilsEnum.Enum2Array<PRODUCT_TYPE>();
        items = new DBproduct[types.Length];
        DBproduct item;
        for (int i = 0, n = types.Length; i < n; i++)
        {
            item = new DBproduct();
            item.type = types[i];
            items[i] = item;

        }
    }

    public DBproduct GetItem(PRODUCT_TYPE type)
    {
        DBproduct item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item;
            }
        }
        return null;
    }
}
