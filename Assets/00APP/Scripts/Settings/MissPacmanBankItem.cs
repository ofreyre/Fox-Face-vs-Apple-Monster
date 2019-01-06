using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MissPacmanBankItem
{
    public UNITTYPE type;
    public float timeFirstDeliver;
    public float timeDeliver;
    public int babies;

    public static MissPacmanBankItem ZERO = new MissPacmanBankItem();

    public MissPacmanBankItem(UNITTYPE type, float timeFirstDeliver, float timeDeliver, int babies)
    {
        this.type = type;
        this.timeFirstDeliver = timeFirstDeliver;
        this.timeDeliver = timeDeliver;
        this.babies = babies;
    }
}
