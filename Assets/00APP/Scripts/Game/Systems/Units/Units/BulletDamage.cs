using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct BulletDamage
{
    public static BulletDamage ZERO;

    public float damageHit;
    public float damageFire;
    public float damageIce;
    public float damageAir;
    public float damageGlue;
    public float durationIce;
    public float durationGlue;

    public BulletDamage(float damageHit, float damageFire, float damageIce, float damageAir, float damageGlue, float durationIce, float durationGlue)
    {
        this.damageHit = damageHit;
        this.damageFire = damageFire;
        this.damageIce = damageIce;
        this.damageAir = damageAir;
        this.damageGlue = damageGlue;
        this.durationIce = durationIce;
        this.durationGlue = durationGlue;
    }

    public override string ToString()
    {
        return "hit = " + damageHit + "  Fire = " + damageFire + "  Ice = " + damageIce + "  air = " + damageAir;
    }
}
