using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesClear : MonoBehaviour
{
    public UpgradesFill m_upgradesFill;
    public FillUpgradesStars m_fillStars;

    // Update is called once per frame
    public void Clear () {
        DBmanager.ClearUpgrades();
        m_upgradesFill.Clear();
        m_fillStars.Fill();
    }
}
