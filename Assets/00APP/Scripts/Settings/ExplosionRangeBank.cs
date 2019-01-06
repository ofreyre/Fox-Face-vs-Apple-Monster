using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRangeBank : ScriptableObject {
    public ExplosionRangeBankItem[] items;

    public ExplosionRangeBankItem GetItem(UNITTYPE type)
    {
        ExplosionRangeBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item;
            }
        }
        return ExplosionRangeBankItem.ZERO;
    }
}
