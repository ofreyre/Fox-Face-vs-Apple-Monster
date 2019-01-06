using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInventoryItemIce : MonoBehaviour
{
    public float m_ammount = 0.07f;

    public void Apply(float duration)
    {
        CollisionManager.instance.ApplyIce(m_ammount, duration);
    }
}
