using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitsBankItem {
    public UNITTYPE type;
    public UnitCellFill[] cellFill;
    public GameObject prefab;
    public BULLETTYPE bulletType;
}
