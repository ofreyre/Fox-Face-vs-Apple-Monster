using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : ScriptableObject
{
    public Color colorIce;
    public Color colorGlue;
    public Color colorBurned;
    public float checkLapseDurationFreeze;
    public float checkLapseDurationSort;
    public float checkLapseDurationCollisions;
    public WaitForSeconds checkLapseFreeze;
    public WaitForSeconds checkLapseSort;
    public WaitForSeconds checkLapseCollisions;

    public void Init()
    {
        checkLapseFreeze = new WaitForSeconds(checkLapseDurationFreeze);
        checkLapseSort = new WaitForSeconds(checkLapseDurationSort);
        checkLapseCollisions = new WaitForSeconds(checkLapseDurationCollisions);
    }
}
