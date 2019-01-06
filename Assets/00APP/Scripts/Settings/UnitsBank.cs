
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsBank : ScriptableObject {
    public BulletsBank bulletsBank;
    public UnitsBankItem[] items;


    public GameObject UNITTYPE_2_GameObject(UNITTYPE type)
    {
        UnitsBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item.prefab;
            }
        }
        return null;
    }

    public BULLETTYPE UNITTYPE_2_BULLETTYPE(UNITTYPE type)
    {
        UnitsBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item.bulletType;
            }
        }
        return BULLETTYPE.none;
    }

    public BulletDamage UNITTYPE_2_Damage(UNITTYPE type)
    {
        UnitsBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return bulletsBank.BULLETTYPE_2_Damage(item.bulletType);
            }
        }
        return BulletDamage.ZERO;
    }

    public UnitsBankItem UNITTYPE_2_Item(UNITTYPE type)
    {
        UnitsBankItem item;
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
