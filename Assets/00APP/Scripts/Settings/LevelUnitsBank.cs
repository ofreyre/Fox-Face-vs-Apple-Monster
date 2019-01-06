using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelUnitsBank : ScriptableObject {
    public UNITTYPE[] available;
    public UNITTYPE[] selected;

    public bool IsItemAvailable(UNITTYPE type)
    {
        return Array.IndexOf(available, type) > -1;
    }

    public bool IsUnitTypeSelected(UNITTYPE type)
    {
        return Array.IndexOf(selected, type) != -1;
    }
}
