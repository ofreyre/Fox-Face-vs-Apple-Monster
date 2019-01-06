using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddIAPcoins : MonoBehaviour, IAddCoins {
    public FillItemsStoreCoins m_coinsUI;

    public void AddCoins(int amount)
    {
        DBmanager.Coins += amount;
        m_coinsUI.Fill();
    }
}
