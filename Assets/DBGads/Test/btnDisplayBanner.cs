using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;
using GoogleMobileAds.Api;

public class btnDisplayBanner : MonoBehaviour {
    BannerView banner;
    AdRequest request;
    AdSDB adsDB;

    public void OnClick (string id) {
        if (banner != null)
        {
            banner.Destroy();
        }

        banner = new BannerView(id, AdSize.Banner, AdPosition.BottomLeft);

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
        banner.Destroy();
    }
}
