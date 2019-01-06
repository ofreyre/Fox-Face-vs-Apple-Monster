using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct InventoryUnitsBankItem {
    public UNITTYPE type;
    public Sprite sprite;
    public int coins;
    public float recover;
    public bool available;
    public int unlockCoins;
    public int order;

    public static InventoryUnitsBankItem NONE = new InventoryUnitsBankItem(UNITTYPE.none, null, 0, 0, false, 0, 0);
    
    public InventoryUnitsBankItem(UNITTYPE type, Sprite sprite, int coins, float recover, bool available, int unlockCoins, int order)
    {
        this.type = type;
        this.sprite = sprite;
        this.coins = coins;
        this.recover = recover;
        this.available = true;
        this.unlockCoins = unlockCoins;
        this.order = order;
    }
}
