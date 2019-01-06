using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUnlockedUnit : MonoBehaviour
{
    public GlobalFlow m_flow;
    public GameStats m_stats;
    public GamepediaBank m_gamepedia;
    public InventoryUnitsBank m_units;
    public GameObject m_messageLose;
    public GameObject m_messageWin;

    public GameObject m_unlockedUI;
    public Text m_unitName;
    public Image m_unitImage;

    public GamepediaDisplayInfo m_displayInfoUI;

    InventoryUnitsBankItem m_item;
    bool m_infoCreated;

    // Use this for initialization
    void Start () {
        if (m_stats.newWonLevel)
        {
            m_item = m_units.GetItemAvailable(m_flow.AbsoluteLevel);
            if (m_item.type != UNITTYPE.none)
            {
                m_unitName.text = m_gamepedia.GetDefender(m_item.type).name;
                m_unitImage.sprite = m_item.sprite;
                m_unitImage.transform.Find("txtPrice").GetComponent<Text>().text = m_item.coins.ToString();
                m_unlockedUI.SetActive(true);
                m_messageLose.SetActive(false);
                m_messageWin.SetActive(false);
            }
            else
            {
                m_unlockedUI.SetActive(false);
                m_messageLose.SetActive(m_stats.Stars == 0);
                m_messageWin.SetActive(m_stats.Stars > 0);
            }
        }
        else
        {
            m_unlockedUI.SetActive(false);
            m_messageLose.SetActive(m_stats.Stars == 0);
            m_messageWin.SetActive(m_stats.Stars > 0);
        }
	}

    public void DisplayInfo()
    {
        if (!m_infoCreated)
        {
            m_infoCreated = true;
            GamepediaDefenderBankItem item = m_gamepedia.GetDefender(m_item.type);
            GameObject thumbnail = Instantiate(item.prefab);
            thumbnail.transform.localScale = Vector3.one;
            GameObject description = Instantiate(item.description);
            description.transform.localScale = Vector3.one;
            m_displayInfoUI.Display(thumbnail, description, item.name);
        }
        m_displayInfoUI.gameObject.SetActive(true);
    }

    public void CloseInfo()
    {
        m_displayInfoUI.gameObject.SetActive(false);
    }
}
