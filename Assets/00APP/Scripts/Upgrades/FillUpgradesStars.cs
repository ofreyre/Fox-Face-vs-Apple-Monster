using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillUpgradesStars : MonoBehaviour
{
    public Text m_stars;

    // Use this for initialization
    void Start () {
        Fill();
    }

    public void Fill()
    {
        m_stars.text = (DBmanager.Stars - DBmanager.Upgrades.TotalStars).ToString();
    }
}
