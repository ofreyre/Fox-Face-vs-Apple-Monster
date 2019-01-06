using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyInventoryItemImmortality : MonoBehaviour
{
    float m_t;

    public void Apply(float amount)
    {
        UnitsSpawner.instance.m_hitK = 0;
        m_t = Mathf.Max(m_t, Time.time) + amount;
        if (!enabled)
        {
            enabled = true;
        }
    }

    void Update()
    {
        if (m_t < Time.time)
        {
            UnitsSpawner.instance.m_hitK = 1;
            enabled = false;
        }
    }
}
