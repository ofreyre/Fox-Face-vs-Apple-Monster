using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInventory : ScriptableObject {
    public UNITTYPE[] m_units;
    public int absoluteLevel = -1;
    public GlobalFlow flow;
    public int coins;
    public int slotsCount;
    public int level;

    public UNITTYPE[] units
    {
        get { return m_units; }
        set {
            absoluteLevel = flow.AbsoluteLevel;
            m_units = value;
        }
    }

    public void ResetUnits()
    {
        m_units = null;
    }
}
