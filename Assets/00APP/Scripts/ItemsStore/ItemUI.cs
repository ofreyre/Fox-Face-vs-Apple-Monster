using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManagement;

public class ItemUI : MonoBehaviour
{
    public DBinventoryItem m_item;
    public Text m_amountUI;
    public Image m_image;
    public string m_btnSFX;
    int m_btnSFX_key;
    int m_amount;

    public virtual void Fill(DBinventoryItem item, int amount) {
        m_item = item;
        Amount = amount;
        m_image.sprite = item.m_inventoryIcon;
        //Enable();
    }
    /*
    public void Fit() {
        transform.localScale = Vector3.one;
        float sX, sY;
        if (m_item.m_inventoryIcon.rect.width > m_item.m_inventoryIcon.rect.height)
        {
            sX = 1;
            sY = m_item.m_inventoryIcon.rect.height / m_item.m_inventoryIcon.rect.width;
        }
        else
        {
            sX = m_item.m_inventoryIcon.rect.width / m_item.m_inventoryIcon.rect.height;
            sY = 1;
        }
        m_image.transform.localScale = new Vector3(sX, sY, 1);
    }*/

    private void Start()
    {
        m_btnSFX_key = m_btnSFX.GetHashCode();
    }

    public int Amount {
        get { return m_amount; }
        set {
            m_amount = value;
            m_amountUI.text = value.ToString();
        }
    }

    public virtual bool Enable(int value = 0) {
        bool enable = m_amount > 0;
        m_image.GetComponent<Button>().interactable = enable;
        return enable;
    }

    public virtual void OnClick() {
        AudioManager.instance.Play(m_btnSFX_key);
        InventoryItemsEvents.DispatchItemSelected(this);
    }
}
