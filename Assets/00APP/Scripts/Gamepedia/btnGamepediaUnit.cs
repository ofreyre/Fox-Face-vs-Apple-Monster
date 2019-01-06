using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnGamepediaUnit : MonoBehaviour
{
    public Text m_text;
    UNITTYPE type;

    public void Fill(InventoryUnitsBankItem item)
    {
        GetComponent<Image>().sprite = item.sprite;
        m_text.text = item.coins.ToString();
        type = item.type;
    }

    public void OnClick()
    {
        GamepediaEvents.DispatchUnitSelected(type);
    }
}
