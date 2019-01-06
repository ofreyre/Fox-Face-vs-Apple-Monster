using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UpgradeCategoryCount
{
    public INVENTORYITEM_CATEGORY category;
    public int count;

    public UpgradeCategoryCount(INVENTORYITEM_CATEGORY category, int count) {
        this.category = category;
        this.count = count;
    }
}

[Serializable]
public struct DBupgrades
{
    public UpgradeCategoryCount[] categoryCounts;

    public UpgradeCategoryCount Categoty2Count(INVENTORYITEM_CATEGORY category) {
        UpgradeCategoryCount count;
        for (int i = 0, n = categoryCounts.Length; i < n; i++) {
            count = categoryCounts[i];
            if (count.category == category) {
                return count;
            }
        }
        return null;
    }

    public void SetCount(INVENTORYITEM_CATEGORY category, int count) {
        Categoty2Count(category).count = count;
    }

    public void AddCount(INVENTORYITEM_CATEGORY category, int count)
    {
        Categoty2Count(category).count += count;
    }

    public int TotalStars {
        get {
            UpgradesCatalog catalog = Resources.Load<UpgradesCatalog>("UpgradesCatalog");
            int stars = 0;
            INVENTORYITEM_CATEGORY category;
            UpgradeItem[] upgradeItems;
            UpgradeCategoryCount count;
            for (int i = 0, n = categoryCounts.Length; i < n; i++)
            {
                count = categoryCounts[i];
                category = count.category;
                upgradeItems = catalog.GetUpgradesCategoryCatalog(category).upgrades;
                for (int j = 0, m = count.count; j < m; j++) {
                    stars += upgradeItems[j].stars;
                }
            }
            return stars;
        }
    }

    //Stars consumed by category
    public int GetStars(INVENTORYITEM_CATEGORY category)
    {
        return Resources.Load<UpgradesCatalog>("UpgradesCatalog").GetUpgradesCategoryCatalog(category).GetStars(Categoty2Count(category).count);
    }

    public void Clear()
    {
        UpgradeCategoryCount count;
        for (int i = 0, n = categoryCounts.Length; i < n; i++)
        {
            count = categoryCounts[i];
            count.count = 0;
            categoryCounts[i] = count;
        }
    }
}
