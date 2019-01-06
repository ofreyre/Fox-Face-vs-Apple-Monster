using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum UPGRADE_TYPE
{
    stamina,
    protection_hit,
    attack_hit,
    attack_ice,
    attack_fire,
    attack_air,
    characterspeed_animator,
    characterspeed_reload,
    ammo_speed,
    ammo_range,
    recovery,
    coin0_value,
    coin1_value,
    coin0_prob,
    coin1_prob,
    coin_duration,
    babypacman_value,
    babypacman_duration,
}

#region Upgrades definitions
[Serializable]
public struct UpgradeItem {
    public UPGRADE_TYPE type;
    public int stars;
    public float amount;
    public ItemUIdata UIdata;
}

[Serializable]
public struct UpgradesCategoryCatalog {
    public INVENTORYITEM_CATEGORY category;
    public UpgradeItem[] upgrades;

    public UpgradesCategoryCatalog(INVENTORYITEM_CATEGORY category, int upgradesCount) {
        this.category = category;
        this.upgrades = new UpgradeItem[upgradesCount];
    }

    public int stars {
        get {
            return GetStars(upgrades.Length);
        }
    }

    public int GetStars(int lastItem) {
        int stars = 0;
        for (int i = 0, n = Mathf.Min(lastItem, upgrades.Length); i < n; i++)
        {
            stars += upgrades[i].stars;
        }
        return stars;
    }
}

public class UpgradesCatalog : ScriptableObject
{
    public UpgradesCategoryCatalog[] upgrades;

    public UpgradesCategoryCatalog GetUpgradesCategoryCatalog(INVENTORYITEM_CATEGORY category)
    {
        UpgradesCategoryCatalog item;
        for (int i = 0, n = upgrades.Length; i < n; i++)
        {
            item = upgrades[i];
            if (item.category == category) {
                return item;
            }
        }
        return new UpgradesCategoryCatalog();
    }
}
#endregion

