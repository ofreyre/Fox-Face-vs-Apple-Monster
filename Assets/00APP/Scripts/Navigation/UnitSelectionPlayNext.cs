using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionPlayNext : MonoBehaviour
{
    public LevelsSettings m_levelSettings;
    public GlobalFlow m_flow;

    public void Play()
    {
        m_flow.AbsoluteLevel += 1;
        LevelSettings settings = m_levelSettings.settings[m_flow.AbsoluteLevel];
        m_flow.ToScene(settings.scene);
    }
}
