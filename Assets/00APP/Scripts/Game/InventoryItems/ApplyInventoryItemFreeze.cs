using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInventoryItemFreeze : MonoBehaviour
{
    public float m_ammount = 10;

    public void Apply(float duration)
    {
        CollisionManager.instance.ApplyIce(m_ammount, duration);
    }
}
