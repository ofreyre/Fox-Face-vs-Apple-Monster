using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MinionCollisionTable
{
    public float limitX, limitY;
}

public struct MinionMoveTable
{
    public float speed, animatorSpeed;
    public float protectionIce, protectionGlue;
}

public struct MinionLifeTable
{
    public float stamina, hit, fire, air;
}

public struct MinionAttackTable
{
    public float hit, range;
    public float idleDuration;
}

public struct MinionsBankItem {
    public ATTACKERTYPE type;
    public GameObject prefab;
    public MinionCollisionTable collision;
    public MinionMoveTable move;
    public MinionLifeTable life;
    public MinionAttackTable attack;
}
