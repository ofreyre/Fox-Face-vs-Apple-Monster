using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInventoryItemHit : MonoBehaviour
{
    public void Apply(float amount)
    {
        CollisionManager.instance.ApplyDamage(new BulletDamage(amount, 0, 0, 0, 0, 0, 0));
    }
}
