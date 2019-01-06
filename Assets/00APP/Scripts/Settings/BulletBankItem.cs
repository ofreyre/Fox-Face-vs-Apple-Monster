using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BulletBankItem {
    public BULLETTYPE type;
    public GameObject prefab;
    public BulletDamage damage;
    public EXPLOSIONTYPE explosionType;
}
