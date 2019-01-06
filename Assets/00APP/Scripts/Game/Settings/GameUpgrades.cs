using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUpgrades : MonoBehaviour
{
    public static GameUpgrades instance;
    public UpgradesCatalog m_upgrades;

    Dictionary<INVENTORYITEM_CATEGORY, Dictionary<UPGRADE_TYPE, float>> m_upgradeAmounts;

    public void Init()
    {
        instance = this;
        OrganizeUpgradesDB();
        ApplyUpgrades();
    }

    void ApplyUpgrades()
    {
        UnitsSpawner.instance.ApplyUpgrades(
            GetAmount(INVENTORYITEM_CATEGORY.life, UPGRADE_TYPE.stamina),
            GetAmount(INVENTORYITEM_CATEGORY.life, UPGRADE_TYPE.protection_hit),
            GetAmount(INVENTORYITEM_CATEGORY.damage, UPGRADE_TYPE.attack_hit),
            GetAmount(INVENTORYITEM_CATEGORY.damage, UPGRADE_TYPE.attack_ice),
            GetAmount(INVENTORYITEM_CATEGORY.damage, UPGRADE_TYPE.attack_fire),
            GetAmount(INVENTORYITEM_CATEGORY.damage, UPGRADE_TYPE.attack_air),
            GetAmount(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.ammo_speed),
            GetAmount(INVENTORYITEM_CATEGORY.ammo, UPGRADE_TYPE.ammo_range),
            (int)GetAmount(INVENTORYITEM_CATEGORY.collectables, UPGRADE_TYPE.babypacman_value),
            GetAmount(INVENTORYITEM_CATEGORY.collectables, UPGRADE_TYPE.babypacman_duration),
            GetAmount(INVENTORYITEM_CATEGORY.characterSpeed, UPGRADE_TYPE.characterspeed_animator),
            GetAmount(INVENTORYITEM_CATEGORY.characterSpeed, UPGRADE_TYPE.characterspeed_reload)
            );

        GameInventory.instance.ApplyUpgrades(GetAmount(INVENTORYITEM_CATEGORY.recovery, UPGRADE_TYPE.recovery));

        BabyPacmanSpawner.instance.ApplyUpgrades(GetAmount(INVENTORYITEM_CATEGORY.characterSpeed, UPGRADE_TYPE.characterspeed_reload));
     }

    void OrganizeUpgradesDB()
    {
        DBupgrades dbUpgrades = DBmanager.Upgrades;
        UpgradesCategoryCatalog[] categoryCatalog = m_upgrades.upgrades;
        UpgradesCategoryCatalog categoryUpgrades;
        INVENTORYITEM_CATEGORY category;
        UpgradeItem[] upgradesCategory;
        UpgradeItem upgrade;
        m_upgradeAmounts = new Dictionary<INVENTORYITEM_CATEGORY, Dictionary<UPGRADE_TYPE, float>>();
        Dictionary<UPGRADE_TYPE, float> upgradeTypeAmount;
        int upgradesCount, upgradeCatalogCount;
        for (int i = 0, n = categoryCatalog.Length; i < n; i++)
        {
            categoryUpgrades = categoryCatalog[i];
            category = categoryUpgrades.category;
            upgradesCategory = categoryUpgrades.upgrades;
            upgradesCount = dbUpgrades.Categoty2Count(category).count;
            upgradeCatalogCount = upgradesCategory.Length;
            upgradeTypeAmount = new Dictionary<UPGRADE_TYPE, float>();
            m_upgradeAmounts.Add(category, upgradeTypeAmount);
            for (int j = 0; j < upgradeCatalogCount; j++)
            {
                upgrade = upgradesCategory[j];
                if (!upgradeTypeAmount.ContainsKey(upgrade.type))
                {
                    upgradeTypeAmount.Add(upgrade.type, 0);
                }
                if (j < upgradesCount)
                {
                    upgradeTypeAmount[upgrade.type] += upgrade.amount;
                }
            }
        }
    }

    public float GetAmount(INVENTORYITEM_CATEGORY category, UPGRADE_TYPE type)
    {

        return m_upgradeAmounts[category][type];
    }
}
