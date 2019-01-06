using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStatesSpeedBank : ScriptableObject {
    public AnimatorUnitStatesSpeedBankItem[] items;

    public AnimatorUnitStatesSpeedBankItem GetItem(UNITTYPE type)
    {
        AnimatorUnitStatesSpeedBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item;
            }
        }
        return AnimatorUnitStatesSpeedBankItem.ZERO;
    }
}
