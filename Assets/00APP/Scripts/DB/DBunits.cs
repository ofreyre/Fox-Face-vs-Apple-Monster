using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct UnitsFixed
{
    public UNITTYPE[] fixedUnits;
    public UNITTYPE[] freeUnits;

    public UnitsFixed(UNITTYPE[] fixedUnits, UNITTYPE[] freeUnits)
    {
        this.fixedUnits = fixedUnits;
        this.freeUnits = freeUnits;
    }
}

[Serializable]
public struct DBunits {
    public List<UNITTYPE> items;

    public DBunits(List<UNITTYPE> units)
    {
        if (units != null)
        {
            this.items = units;
        }
        else
        {
            this.items = new List<UNITTYPE>();
            this.items.Add(UNITTYPE.peashooter);
        }
    }

    public void Add(UNITTYPE unit)
    {
        if (!items.Contains(unit))
        {
            items.Add(unit);
        }
    }

    public bool Contains(UNITTYPE unit)
    {
        return items.Contains(unit);
    }

    public int Count
    {
        get { return items.Count; }
    }

    public UnitsFixed GetFixed(int fixedCount, int minFreeUnits)
    {
        List<UNITTYPE> freeUnits = new List<UNITTYPE>(items);
        List<UNITTYPE> fixedUnits = new List<UNITTYPE>();
        int i;
        while (fixedUnits.Count < fixedCount)
        {
            i = UnityEngine.Random.Range(0, freeUnits.Count);
            fixedUnits.Add(freeUnits[i]);
            freeUnits.RemoveAt(i);
        }
        return new UnitsFixed(fixedUnits.ToArray(), freeUnits.ToArray());
    }
}
