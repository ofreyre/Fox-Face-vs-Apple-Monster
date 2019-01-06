using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour {
    public FillInventory m_inventory;

    void Start() {
        InventoryItemsEvents.instance.ItemSelected += ItemSelected;
    }

    void ItemSelected(ItemUI item)
    {
        gameObject.SetActive(true);
        m_inventory.Display(false);
        if (item == null)
        {
            InventoryItemsEvents.instance.ItemSelected -= ItemSelected;
            GetComponent<Button>().interactable = false;
        }
    }

	// Use this for initialization
	public void OnClick () {
        gameObject.SetActive(false);
        m_inventory.Display(true);
    }
}
