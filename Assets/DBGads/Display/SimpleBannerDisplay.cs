using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;
using GoogleMobileAds.Api;
using System;

public class SimpleBannerDisplay : MonoBehaviour
{
    public string m_androidID;
    public string m_iosID;
    string m_unitID;
    static BannerView banner;
    static AD_STATE state = AD_STATE.idle;
    static bool display;
    AdRequest request;
    AdSDB adsDB;

    void Start()
    {
        Show();
    }

    public virtual void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        state = AD_STATE.failToLoad;
    }

    public virtual void HandleOnAdLoaded(object sender, EventArgs args)
    {
        state = AD_STATE.loaded;
        if (!display)
        {
            banner.Hide();
        }
    }

    public void Hide()
    {
        display = false;
        switch (state)
        {
            case AD_STATE.failToLoad:
                state = AD_STATE.idle;
                break;
            case AD_STATE.loaded:
                state = AD_STATE.idle;
                if (banner != null)
                {
                    banner.Destroy();
                }
                break;
        }
    }

    public void Show()
    {
        display = true;

        switch (state)
        {
            case AD_STATE.idle:
            case AD_STATE.failToLoad:
                Load();
                break;
            default:
                if (banner != null)
                {
                    banner.Show();
                }
                else
                {
                    Load();
                }
                break;
        }
    }

    public void Load()
    {
        display = true;
        state = AD_STATE.loading;
#if UNITY_ANDROID
        m_unitID = m_androidID;
#elif UNITY_IPHONE
        m_unitID = m_iosID;
#else
        m_unitID = "unexpected_platform";
#endif
        if (banner != null)
        {
            banner.Destroy();
        }

        banner = new BannerView(m_unitID, AdSize.Banner, AdPosition.BottomLeft);

        banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        banner.OnAdLoaded += HandleOnAdLoaded;

        adsDB = AdSDB.Get();
        if (adsDB.consent == ADS_CONSENT.relevant)
        {
            request = new AdRequest.Builder().Build();
        }
        else
        {
            request = new AdRequest.Builder().AddExtra("npa", "1").Build();
        }
        request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }

    public void OnDestroy()
    {
        Hide();
    }
}
