using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text m_scoreText;
    [HideInInspector]
    public int m_score;
    Hordes m_hordes;
    Dictionary<ATTACKERTYPE, int> m_points;


    public void Init(Hordes hordes)
    {
        m_hordes = hordes;
        instance = this;
        GameEvents.instance.AttackerKilled += OnAttackerKilled;
        m_points = m_hordes.Points;
        m_scoreText.text = m_score.ToString();
    }

    public void OnAttackerKilled(Vector3 pos, ATTACKERTYPE type)
    {
        m_score += m_points[type];
        m_scoreText.text = m_score.ToString();
    }
}
