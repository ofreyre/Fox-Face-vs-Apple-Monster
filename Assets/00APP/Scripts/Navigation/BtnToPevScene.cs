using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnToPevScene : ButtonLoadScene
{
    // Use this for initialization
    public void LoadPrev ()
    {
        LoadScene(m_flow.PrevScene);
    }
}
