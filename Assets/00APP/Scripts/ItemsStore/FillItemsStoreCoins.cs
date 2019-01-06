using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillItemsStoreCoins : MonoBehaviour {

    public Text m_text;

    // Use this for initialization
    void Start()
    {
        Fill();
    }

    public void Fill()
    {
        m_text.text = DBmanager.Coins.ToString();
    }
}
