using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissPacmanBank : ScriptableObject {
    public MissPacmanBankItem[] units;

    public MissPacmanBankItem GetItem(UNITTYPE type)
    {
        MissPacmanBankItem item;
        for (int i = 0, n = units.Length; i < n; i++)
        {
            item = units[i];
            if (item.type == type)
            {
                return item;
            }
        }
        return MissPacmanBankItem.ZERO;
    }
}
