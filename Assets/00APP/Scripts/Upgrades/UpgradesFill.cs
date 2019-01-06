using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using AudioManagement;

public class UpgradesFill : MonoBehaviour {
    [Serializable]
    public struct CathegoryIcons{
        public INVENTORYITEM_CATEGORY category;
        public Transform icon;
    }

    public UpgradesCatalog m_catalog;
    //public CathegoryIcons[] m_categoryIcons;
    public GameObject m_prefab;
    public Transform m_listUI;
    public Transform m_listContainer;
    public Text m_stars;
    public Button m_btnAdd;
    public string m_collectSFX;
    public string m_sellectSFX;
    int m_collectSFX_key;
    int m_sellectSFX_key;

    INVENTORYITEM_CATEGORY m_upgradeCategory;
    int m_upgradeIndex = -1;
    bool m_add;

    Dictionary<INVENTORYITEM_CATEGORY, UpgradeItemUI[]> m_itemsUI;
    

    // Use this for initialization
    void Start () {

        m_collectSFX_key = m_collectSFX.GetHashCode();
        m_sellectSFX_key = m_sellectSFX.GetHashCode();

        UpgradesEvents.instance.UpgradeSelected += SelectUpgrade;

        UpgradesCategoryCatalog[] categoryCatalog = m_catalog.upgrades;
        DBupgrades dbUpgrades = DBmanager.Upgrades;
        m_itemsUI = new Dictionary<INVENTORYITEM_CATEGORY, UpgradeItemUI[]>();

        UpgradesCategoryCatalog category;
        //Transform icon;
        for (int i = 0, n = categoryCatalog.Length; i < n; i++)
        {
            category = categoryCatalog[i];
            m_itemsUI.Add(category.category, new UpgradeItemUI[category.upgrades.Length]);
            /*icon = Category2Icon(category.category);
            if (icon != null) {
                icon.SetParent(m_listUI);
                icon.localScale = Vector3.one;
            }*/
        }
        UpdateList(true);
        //m_listUI.parent = m_listContainer;
    }

    /*Transform Category2Icon(INVENTORYITEM_CATEGORY category)
    {
        CathegoryIcons icon;
        for (int i = 0, n = m_categoryIcons.Length; i < n; i++)
        {
            icon = m_categoryIcons[i];
            if (icon.category == category) {
                return Instantiate(icon.icon).transform;
            }
        }
        return null;
    }*/

    public void SelectUpgrade(INVENTORYITEM_CATEGORY category, int index, bool add)
    {
        if (category != INVENTORYITEM_CATEGORY.none)
        {
            AudioManager.instance.Play(m_sellectSFX_key);
            if (m_upgradeIndex != -1)
            {
                m_itemsUI[m_upgradeCategory][m_upgradeIndex].UnSelect();
            }
            m_add = add;
            m_upgradeCategory = category;
            m_upgradeIndex = index;
            m_itemsUI[category][m_upgradeIndex].Select();
            m_btnAdd.interactable = m_itemsUI[category][m_upgradeIndex].State == btnItem.btnItem_State.enabled;
        }
    }

    public void AddUpgrade() {
        if (m_add)
        {
            AudioManager.instance.Play(m_collectSFX_key);
            DBmanager.AddUpgrade(m_upgradeCategory, 1);
            UpdateList(false);
            m_stars.text = (DBmanager.Stars - DBmanager.Upgrades.TotalStars).ToString();
            m_btnAdd.interactable = false;
            m_itemsUI[m_upgradeCategory][m_upgradeIndex].Select();
            //m_upgradeIndex = -1;
            m_add = false;
        }
    }

    public void Clear()
    {
        UpgradesEvents.instance.DispatchUpgradeSelected(INVENTORYITEM_CATEGORY.none, 0, false);
        UpdateList(false);
        m_upgradeIndex = -1;
        m_btnAdd.interactable = false;
    }

    public void UpdateList(bool fill)
    {
        DBupgrades dbUpgrades = DBmanager.Upgrades;
        //Upgrades of user per category
        
        //Total of user not consumed
        int totalStars = DBmanager.Stars - dbUpgrades.TotalStars;

        //Catalog upgrades per category
        UpgradesCategoryCatalog[] categoryCatalog = m_catalog.upgrades;
        UpgradesCategoryCatalog category;

        int[] userStarsPerCategory = new int[categoryCatalog.Length];
        for (int i = 0, n = categoryCatalog.Length; i < n; i++) {
            category = categoryCatalog[i];
            userStarsPerCategory[i] = dbUpgrades.GetStars(category.category);
        }

        UpgradeItem[] upgrades;
        int j = 0;
        UpgradeItemUI itemUI;
        UpgradeItem item;

        
        //Star
        int userCategoryStars;

        bool repeat = true;
        while (repeat)
        {
            repeat = false;
            for (int i = 0, n = categoryCatalog.Length; i < n; i++)
            {
                category = categoryCatalog[i];
                upgrades = category.upgrades;
                if (j < upgrades.Length)
                {
                    repeat = true;
                    item = upgrades[j];
                    if (fill)
                    {
                        itemUI = Instantiate(m_prefab).GetComponent<UpgradeItemUI>();
                        itemUI.transform.Find("icon").GetComponent<Image>().sprite = item.UIdata.m_sprite;
                        itemUI.transform.SetParent(m_listUI);
                        itemUI.transform.localScale = Vector3.one;
                        itemUI.m_category = category.category;
                        itemUI.m_index = j;
                        itemUI.FIll(j, item.stars);
                        m_itemsUI[category.category][j] = itemUI;
                    }
                    else {
                        itemUI = m_itemsUI[category.category][j];
                    }
                    userCategoryStars = userStarsPerCategory[i];
                    if (item.stars <= userCategoryStars)
                    {
                        itemUI.State = btnItem.btnItem_State.used;
                        userStarsPerCategory[i] -= item.stars;
                        //userStarsPerCategory[i] = 0;
                        //itemUI.Disabled();
                    }
                    else if (userCategoryStars == 0)
                    {
                        userStarsPerCategory[i] = -1;
                        if (item.stars <= totalStars)
                        {
                            itemUI.State = btnItem.btnItem_State.enabled;
                        }
                        else
                        {
                            itemUI.State = btnItem.btnItem_State.disabled;
                        }
                    }
                    else
                    {
                        itemUI.State = btnItem.btnItem_State.disabled;
                    }
                }
            }
            j++;
        }
    }
}

