using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveWin : MonoBehaviour
{
    public LevelsSettings m_levelSettings;
    public GlobalFlow m_flow;
    public Globals m_globals;
    public bool m_active;
    public LoadSceneAfterTime m_loadSceneAfterTime;

    // Use this for initialization
    void Start () {
        if (m_flow.AbsoluteLevel > m_globals.LevelsCount - 2 && !DBmanager.Win)
        {
            DBmanager.Win = true;
            m_loadSceneAfterTime.enabled = true;
        }
        else
        {
            m_loadSceneAfterTime.enabled = false;
        }
    }
}
