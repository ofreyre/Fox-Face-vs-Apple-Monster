using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScrollAndLoadScene : ButtonLoadScene
{
    public Levels m_levels;
    public override void LoadScene(string scene)
    {
        m_levels.SaveScrollPosition();
        base.LoadScene(scene);
    }
}
