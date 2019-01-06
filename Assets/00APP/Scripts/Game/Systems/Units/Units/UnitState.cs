using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNITSTATE
{
    alife,
    dead
}

public class UnitState : MonoBehaviour {
    public UNITSTATE m_state;
    public float staminaMax = 1;
    public float stamina;
    UnitsSpawner m_unitSpawner;

    private void Awake()
    {
        m_unitSpawner = UnitsSpawner.instance;
    }

    void OnEnable () {
        m_state = UNITSTATE.alife;
        stamina = staminaMax;
    }

    public bool Hit(float amount)
    {
        stamina -= amount * m_unitSpawner.m_hitK;
        return stamina > 0;
    }
}
