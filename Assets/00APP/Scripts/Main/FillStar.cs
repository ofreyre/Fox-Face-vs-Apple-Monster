using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStar : MonoBehaviour
{
    public Globals m_globals;
    public Text m_stars;

    void Start()
    {
        Fill();
    }

    public virtual void Fill()
    {
        m_stars.text = DBmanager.Stars + "/" + m_globals.StarsCount.ToString();
    }
}
