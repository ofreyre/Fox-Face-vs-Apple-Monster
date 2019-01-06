using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsBank : ScriptableObject
{
    public ExplosionBankItem[] items;

    public GameObject BULLETTYPE_2_GameObject(EXPLOSIONTYPE type)
    {
        ExplosionBankItem item;
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
}
