using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillUpgradeUI : MonoBehaviour
{
    public UpgradesCatalog m_catalog;
    public Text m_name;
    public Text m_stars;
    public Text m_description;

    // Use this for initialization
    void Start ()
    {
        UpgradesEvents.instance.UpgradeSelected += SelectUpgrade;
    }

    public void SelectUpgrade(INVENTORYITEM_CATEGORY category, int index, bool add)
    {
        if (category != INVENTORYITEM_CATEGORY.none)
        {
            UpgradeItem item = m_catalog.GetUpgradesCategoryCatalog(category).upgrades[index];
            ItemUIdata UIdata = item.UIdata;
            m_name.text = UIdata.m_name;
            m_stars.gameObject.SetActive(true);
            m_stars.text = item.stars.ToString();
            m_description.text = UIdata.m_description;
        }
        else
        {
            m_name.text = "Use your stars wisely";
            m_stars.gameObject.SetActive(false);
            m_description.text = "";
        }
    }
}
