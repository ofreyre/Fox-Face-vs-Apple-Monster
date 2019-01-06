using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings : ScriptableObject
{
    public int cellsX = 10, cellsY = 6;
    public Sprite sprite;
    public Sprite spriteTop;
    public Vector3 cellPivot;
    public Vector2 rightBottomMargin = new Vector2(1, 0);
    public Vector2 leftTopMarginMin = new Vector2(1.5f, 1);
}
