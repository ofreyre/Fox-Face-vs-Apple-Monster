using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillItemUI : MonoBehaviour
{
    public Text m_name;
    public Text m_coins;
    public Text m_description;
    public Transform m_scroll;
    public GameObject m_message;
    GameObject m_unitDescription;

    // Use this for initialization
    void Start ()
    {
        InventoryItemsEvents.instance.ItemSelected += Selected;
        InventoryItemsEvents.instance.ItemUnitSelected += Selected;
        InventoryItemsEvents.instance.ItemUnitRemoved += OnItemUnitRemoved;
    }
	
	void Selected(ItemUI itemUI)
    {
        m_name.text = itemUI.m_item.name;
        m_coins.text = itemUI.m_item.price.ToString();
        m_coins.gameObject.SetActive(true);
        m_description.text = itemUI.m_item.description;
        m_description.gameObject.SetActive(true);
        if (m_unitDescription != null)
        {
            Destroy(m_unitDescription);
        }
        m_message.SetActive(false);
    }

    void Selected(ItemUnitUI itemUI)
    {
        m_name.text = itemUI.m_description.name;
        m_coins.text = itemUI.m_item.unlockCoins.ToString();
        m_coins.gameObject.SetActive(true);
        m_description.gameObject.SetActive(false);
        //m_description.text = itemUI.m_item.description;

        if (m_unitDescription != null)
        {
            Destroy(m_unitDescription);
        }

        m_unitDescription = Instantiate(itemUI.m_description.description);
        m_unitDescription.transform.SetParent(m_scroll);
        m_unitDescription.transform.localScale = Vector3.one;
        m_unitDescription.transform.localPosition = new Vector3(0,-150,0);
        m_message.SetActive(true);
    }

    void OnItemUnitRemoved()
    {
        m_name.text = "Use your coins wisely";
        if (m_unitDescription != null)
        {
            Destroy(m_unitDescription);
        }
        m_message.SetActive(false);
    }
}
