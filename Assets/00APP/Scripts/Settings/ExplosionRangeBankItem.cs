using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ExplosionRangeBankItem {
    public UNITTYPE type;
    public int rowRange;
    public float columnRangeMin;
    public float columnRangeMax;
    public float columnDetectionMin;
    public float columnDetectionMax;
    public BulletDamage damage;

    public ExplosionRangeBankItem(UNITTYPE type, int rowRange, float columnRangeMin, float columnRangeMax, float columnDetectionMin, float columnDetectionMax, BulletDamage damage)
    {
        this.type = type;
        this.rowRange = rowRange;
        this.columnRangeMin = columnRangeMin;
        this.columnRangeMax = columnRangeMax;
        this.columnDetectionMin = columnDetectionMin;
        this.columnDetectionMax = columnDetectionMax;
        this.damage = damage;
    }

    public static ExplosionRangeBankItem ZERO = new ExplosionRangeBankItem(UNITTYPE.none, 0, 0, 0, 0, 0, BulletDamage.ZERO);
}
