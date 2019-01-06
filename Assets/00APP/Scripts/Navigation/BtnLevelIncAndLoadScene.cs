using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLevelIncAndLoadScene : MonoBehaviour
{
    public GlobalFlow m_flow;

    public virtual void LoadScene(string scene)
    {
        m_flow.AbsoluteLevel++;
        m_flow.ToScene(scene);
    }
}
