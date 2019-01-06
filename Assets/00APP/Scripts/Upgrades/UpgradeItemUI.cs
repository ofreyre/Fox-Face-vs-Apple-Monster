using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemUI : btnItemOneText
{

    public INVENTORYITEM_CATEGORY m_category;
    public int m_index;

    public void FIll(int index, int stars) {
        m_index = index;
        Fill(stars.ToString());
    }

    public void AddUpgrade() {
        UpgradesEvents.instance.DispatchUpgradeSelected(m_category, m_index, consumable);
    }
}
