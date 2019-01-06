using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : ScriptableObject
{
    public GlobalFlow flow;
    public Globals m_globals;
    public int babyPacmans;
    [SerializeField]
    int score;
    [SerializeField]
    int coins;
    [SerializeField]
    int stars;
    public bool newWonLevel;

    public void SetStars(int rows, int defencesUsed = -1)
    {
        int oldStars = DBmanager.GetStars(flow.AbsoluteLevel);
        if (defencesUsed > -1)
        {            
            if (defencesUsed == 0)
            {
                stars = 3;
            }
            else if (defencesUsed <= (int)Mathf.Ceil(rows / 3f))
            {
                stars = 2;
            }
            else
            {
                stars = 1;
            }
        }
        else
        {
            stars = 0;
        }
        DBmanager.SaveStars(flow.AbsoluteLevel, stars);
        newWonLevel = oldStars == 0 && stars > 1;
    }

    public int Score
    {
        get {
            return score;
        }

        set
        {
            score = value;
            DBmanager.SaveScore(flow.level, score);
            DBmanager.Coins += value / m_globals.pointsPerCoin;
        }
    }

    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            DBmanager.Coins += coins;
        }
    }

    public int Stars
    {
        get { return stars; }
        set
        {
            int oldStars = DBmanager.GetStars(flow.AbsoluteLevel);
            stars = value;
            DBmanager.SaveStars(flow.AbsoluteLevel, value);
            newWonLevel = oldStars == 0 && stars > 1;
        }
    }
}
