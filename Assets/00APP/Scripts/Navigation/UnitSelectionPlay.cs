using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionPlay : MonoBehaviour {
    public LevelsSettings m_levelSettings;
    public GlobalFlow m_flow;

    public void Play()
    {
        LevelSettings settings = m_levelSettings.settings[m_flow.AbsoluteLevel];
        m_flow.ToScene(settings.scene);
    }
}
