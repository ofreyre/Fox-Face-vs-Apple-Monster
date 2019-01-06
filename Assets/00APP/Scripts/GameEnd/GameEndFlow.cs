using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndFlow : MonoBehaviour {
    public static GameEndFlow instance;
    public delegate void DelegateStepEnd();
    public event DelegateStepEnd StepEnd;

    public GlobalFlow m_flow;
    public FillStatsAnimated m_animatedScore;
    public AnimateStars m_animateStars;

    int i;

    private void Awake()
    {
        instance = this;
    }

    public static void DispatchStepEnd()
    {
        if (instance.StepEnd != null)
        {
            instance.StepEnd();
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_animatedScore.FillCoinsCollected();
        if (m_flow.PrevScene.Contains("Game"))
        {

            StepEnd += OnStepEnd;
            m_animateStars.Init();
        }
        else
        {
            m_animateStars.Fill();
            OnStepEnd();
        }

    }

    void OnStepEnd()
    {
        m_animatedScore.Init();
    }
}
