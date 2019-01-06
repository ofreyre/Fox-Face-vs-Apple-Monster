using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatsAnimated : MonoBehaviour
{
    public GlobalFlow m_flow;
    public Globals m_globals;
    public GameStats m_gameStats;
    public MoveYToTargetCyclic m_animatedCoin;
    public Text m_scoreTxt;
    public Text m_coinsTxt;
    float m_duration = 3;
    float m_incLapse = 0.1f;
    int m_coins;

    // Use this for initialization
    public void Init ()
    {
        m_coins = m_gameStats.Coins;
        if (m_flow.PrevScene.Contains("Game"))
        {
            StartCoroutine(Animate());
        }
        else
        {
            Fill();
        }
    }

    IEnumerator Animate()
    {
        int dbScore = m_gameStats.Score;
        float scoreInc = dbScore * m_incLapse / m_duration;
        m_scoreTxt.text = "0";
        m_coinsTxt.text = m_coins.ToString();
        float score = 0;
        float pointsPerCoin = m_globals.pointsPerCoin;
        float lastCoinScore = pointsPerCoin;
        bool statsAnimationStarted = false;
        while (score < dbScore)
        {
            yield return new WaitForSeconds(m_incLapse);
            score += scoreInc;
            m_scoreTxt.text = ((int)score).ToString();
            if (score >= lastCoinScore)
            {
                if (!statsAnimationStarted)
                {
                    statsAnimationStarted = true;
                    m_animatedCoin.Animate();
                }
                lastCoinScore += pointsPerCoin;
                m_coinsTxt.text = ((int)(m_coins + score / pointsPerCoin)).ToString();
            }
        }
        Fill();
        m_animatedCoin.gameObject.SetActive(false);
    }

    public void FillCoinsCollected()
    {
        m_coinsTxt.text = m_gameStats.Coins.ToString();
    }

    public void Fill()
    {
        m_scoreTxt.text = m_gameStats.Score.ToString();
        m_coinsTxt.text = (m_coins + m_gameStats.Score / m_globals.pointsPerCoin).ToString();
    }

}
