using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInventoryItemBabypacman : MonoBehaviour
{
    public void Apply(float amount)
    {
        GameInventory.instance.coins += (int)amount;
    }
}
