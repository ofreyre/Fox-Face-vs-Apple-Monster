using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicPacman : MonoBehaviour
{
    public GameObject m_play;

    public void SetPlay(bool play)
    {
        m_play.SetActive(play);
        enabled = play;
    }
}
