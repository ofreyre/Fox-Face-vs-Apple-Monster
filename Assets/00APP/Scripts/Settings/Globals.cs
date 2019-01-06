using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : ScriptableObject
{
    public int stages = 2;
    public int levelsPerStage = 30;
    public int coinsPerVideo = 100;
    public int pointsPerCoin = 100;
    public int starsPerLevel = 3;

    public int LevelsCount {
        get { return stages * levelsPerStage; }
    }

    public int StarsCount { get { return LevelsCount * starsPerLevel; } }
}
