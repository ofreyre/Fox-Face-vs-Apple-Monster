using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepediaBank : ScriptableObject
{
    public GamepediaAttackerBankItem[] attackers;
    public GamepediaDefenderBankItem[] defenders;
    GamepediaAttackerBankItem localAttacker;
    GamepediaDefenderBankItem localDefender;

    public GamepediaAttackerBankItem GetAttacker(ATTACKERTYPE type)
    {
        for (int i = 0, n = attackers.Length; i < n; i++)
        {
            localAttacker = attackers[i];
            if (localAttacker.type == type)
            {
                return localAttacker;
            }
        }
        return null;
    }

    public GamepediaDefenderBankItem GetDefender(UNITTYPE type)
    {
        for (int i = 0, n = defenders.Length; i < n; i++)
        {
            localDefender = defenders[i];
            if (localDefender.type == type)
            {
                return localDefender;
            }
        }
        return null;
    }
}
