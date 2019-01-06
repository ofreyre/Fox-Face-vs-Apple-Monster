using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveIfNotLast : MonoBehaviour
{
    public LevelsSettings m_levelSettings;
    public GlobalFlow m_flow;
    public Globals m_globals;
    public bool m_ifNotLast = true;

    void Start()
    {
        if (m_ifNotLast)
        {
            if (m_flow.AbsoluteLevel > m_globals.LevelsCount - 2)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (m_flow.AbsoluteLevel < m_globals.LevelsCount - 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
