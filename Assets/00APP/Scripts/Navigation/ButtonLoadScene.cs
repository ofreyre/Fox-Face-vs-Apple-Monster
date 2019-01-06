using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public GlobalFlow m_flow;

    public virtual void LoadScene(string scene)
    {
        m_flow.ToScene(scene);
    }
}
