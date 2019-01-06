using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DBGads;

public class EarnUnitSlotController : MonoBehaviour
{
    [SerializeField]
    SimpleBannerDisplay m_bannerDisplay;

    void Start()
    {
        WatchVideo.instance.Loaded += OnStarted;
        WatchVideo.instance.Close += OnClose;
        WatchVideo.instance.Fail += OnFail;
        WatchVideo.instance.Reward += OnReward;
    }

    void OnStarted()
    {
        if (m_bannerDisplay != null)
        {
            m_bannerDisplay.Hide();
        }
    }

    void OnClose()
    {
        if (m_bannerDisplay != null)
        {
            m_bannerDisplay.Show();
        }
    }

    void OnFail()
    {
        EventManagerMessages.instance.DispatchMessage("Sorry, no video available.\nPlease try again.");
    }

    void OnReward()
    {
        EventManagerMessages.instance.DispatchMessage("Congratulations, you earned one extra slot.");
    }
}
