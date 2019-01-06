using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInventoryItemAir : MonoBehaviour
{
    public void Apply(float amount)
    {
        CollisionManager.instance.ApplyDamage(new BulletDamage(0, 0, 0, amount, 0, 0, 0));
    }
}
