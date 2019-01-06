using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBank : ScriptableObject
{
    public BulletBankItem[] items;

    public GameObject BULLETTYPE_2_GameObject(BULLETTYPE type)
    {
        BulletBankItem item;
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

    public BulletDamage BULLETTYPE_2_Damage(BULLETTYPE type)
    {
        BulletBankItem item;
        for (int i = 0, n = items.Length; i < n; i++)
        {
            item = items[i];
            if (item.type == type)
            {
                return item.damage;
            }
        }
        return BulletDamage.ZERO;
    }

    public BulletBankItem BULLETTYPE_2_Item(BULLETTYPE type)
    {
        BulletBankItem item;
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
