using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;

public class GlobalFlow : ScriptableObject
{
    public Globals globals;
    public int stage;
    public int level;
    public string firstScene;
    public bool toPlay;
    public string interstitial;
    public string gamePrefix;
    string prevScene = null;
    string currentScene = null;

    public void Init()
    {
        prevScene = null;
        currentScene = null;
    }

    public int AbsoluteLevel {
        set {
            stage = value / globals.levelsPerStage;
            level = value % globals.levelsPerStage;
        }
        get {
            return stage * globals.levelsPerStage + level;
        }
    }

    public void ToScene(string name)
    {
        prevScene = currentScene;
        currentScene = name;
        //System.GC.Collect();
        Application.LoadLevel(name);
    }

    public string PrevScene {
        get
        {
            if (prevScene == null) {
                prevScene = firstScene;
            }
            return prevScene;
        }
    }
}
