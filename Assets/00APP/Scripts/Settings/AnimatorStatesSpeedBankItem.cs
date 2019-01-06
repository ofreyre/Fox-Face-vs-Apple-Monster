using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AnimatorStatesSpeedBankItem {
    public AnimatorController.ANIMATORSTATES state;
    public float duration;

    public static AnimatorStatesSpeedBankItem ZERO = new AnimatorStatesSpeedBankItem();
}

[Serializable]
public struct AnimatorUnitStatesSpeedBankItem
{
    public UNITTYPE type;
    public AnimatorStatesSpeedBankItem[] items;

    public static AnimatorUnitStatesSpeedBankItem ZERO = new AnimatorUnitStatesSpeedBankItem();

    public AnimatorStatesSpeedBankItem GetItem(AnimatorController.ANIMATORSTATES state)
    {
        AnimatorStatesSpeedBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.state == state)
            {
                return item;
            }
        }
        return AnimatorStatesSpeedBankItem.ZERO;
    }
}
