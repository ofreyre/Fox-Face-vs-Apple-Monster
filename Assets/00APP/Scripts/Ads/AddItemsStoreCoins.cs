using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemsStoreCoins : MonoBehaviour, IAddCoins {

    public ItemsStore m_itemsStore;

    public void AddCoins(int amount)
    {
        m_itemsStore.AddCoins(amount);
    }
}
