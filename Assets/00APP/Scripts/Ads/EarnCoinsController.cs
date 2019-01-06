using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DBGads;

public class EarnCoinsController : MonoBehaviour
{
    [SerializeField]
    GameObject m_btnWatch;
    [SerializeField]
    SimpleBannerDisplay m_bannerDisplay;
    [SerializeField]
    Globals m_globals;

    void Start ()
    {
        //WatchVideo.instance.Ready += OnReady;
        WatchVideo.instance.Request += OnRequest;
        WatchVideo.instance.Loaded += OnStarted;
        WatchVideo.instance.Close += OnClose;
        WatchVideo.instance.Fail += OnFail;
        WatchVideo.instance.Reward += OnReward;
    }

    void OnReady()
    {
        m_btnWatch.SetActive(true);
        m_btnWatch.GetComponent<Button>().interactable = true;
    }

    void OnRequest()
    {
        m_btnWatch.GetComponent<Button>().interactable = false;
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
        m_btnWatch.SetActive(true);
        m_btnWatch.GetComponent<Button>().interactable = true;
    }

    void OnFail()
    {
        if (m_bannerDisplay != null)
        {
            m_bannerDisplay.Show();
        }
        m_btnWatch.SetActive(true);
        m_btnWatch.GetComponent<Button>().interactable = true;
        EventManagerMessages.instance.DispatchMessage("Sorry, no video available.");
    }

    void OnReward()
    {
        if (m_bannerDisplay != null)
        {
            m_bannerDisplay.Show();
        }
        GetComponent<IAddCoins>().AddCoins(m_globals.coinsPerVideo);
        m_btnWatch.SetActive(true);
        m_btnWatch.GetComponent<Button>().interactable = true;
        EventManagerMessages.instance.DispatchMessage("Congratulations, you earned "+ m_globals .coinsPerVideo+ " coins.");
    }

    private void OnDestroy()
    {
        if (WatchVideo.instance != null)
        {
            WatchVideo.instance.Request -= OnRequest;
            WatchVideo.instance.Loaded -= OnStarted;
            WatchVideo.instance.Close -= OnClose;
            WatchVideo.instance.Fail -= OnFail;
            WatchVideo.instance.Reward -= OnReward;
        }
    }
}
