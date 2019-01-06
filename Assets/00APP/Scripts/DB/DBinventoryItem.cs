using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum INVENTORYITEM_TYPE
{
    immortality_1,
    immortality_2,
    immortality_3,
    immortality_4,
    damage_hit_1,
    damage_hit_2,
    damage_hit_3,
    damage_hit_4,
    damage_ice_1,
    damage_ice_2,
    damage_ice_3,
    damage_ice_4,
    damage_fire_1,
    damage_fire_2,
    damage_fire_3,
    damage_fire_4,
    damage_air_1,
    damage_air_2,
    damage_air_3,
    damage_air_4,
    freeze_1,
    freeze_2,
    freeze_3,
    freeze_4,
    character_speedup_1,
    character_speedup_2,
    character_speedup_3,
    character_speedup_4,
    babypacman_1,
    babypacman_2,
    babypacman_3,
    babypacman_4,
    babypacman_5,
    none
}

public enum INVENTORYITEM_ITEM_CATEGORY
{
    immortality,
    damage_hit,
    damage_ice,
    damage_fire,
    damage_air,
    freeze,
    character_speedup,
    babypacman,
    none
}

public enum INVENTORYITEM_CATEGORY{
    life,
    damage,
    characterSpeed,
    ammo,
    recovery,
    collectables,
    none
}

[Serializable]
public class DBinventoryItem: ScriptableObject {
    public INVENTORYITEM_TYPE type;
    public INVENTORYITEM_ITEM_CATEGORY category;
    public float amount;
    public new string name;
    public string description;
    public Sprite m_inventoryIcon;
    public int price;
    public int inventoryMax;
    public bool visibleInInventory = true;

    public static int CompareDBinventoryItem(DBinventoryItem a, DBinventoryItem b) {
        if (a.category < b.category) return -1;
        else if (a.category > b.category) return 1;
        else
        {
            if (a.price < b.price) return -1;
            else if (a.price > b.price) return 1;
            return 0;
        }
    }
}
