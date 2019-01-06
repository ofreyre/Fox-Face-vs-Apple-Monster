using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

namespace DBGads
{
    public enum AD_STATE
    {
        idle,
        loading,
        loaded,
        opened,
        failToLoad,
        started,
        rewarded
    }

    public class WatchVideo : MonoBehaviour
    {
        [SerializeField]
        float m_waitForResponse = 5;

        public string m_androidID;
        public string m_iosID;

        public float m_retryDelay = 1f;

        string m_unitID;
        static RewardBasedVideoAd rewardBasedVideo;
        AdRequest request;
        AdSDB adsDB;

        static bool m_messageDisplayed;
        static bool display = false;
        static bool m_rewarded;
        static AD_STATE m_state;

        WaitForSeconds m_retryWait;
        WaitForSeconds m_responseWait;
        Coroutine m_WaitForResponse;

        public delegate void DelegateReady();
        public event DelegateReady Ready;
        public delegate void DelegateRequest();
        public event DelegateRequest Request;
        public delegate void DelegateOpen();
        public event DelegateOpen Loaded;
        public delegate void DelegateStarted();
        public event DelegateStarted Started;
        public delegate void DelegateClose();
        public event DelegateClose Close;
        public delegate void DelegateFail();
        public event DelegateFail Fail;
        public delegate void DelegateReward();
        public event DelegateReward Reward;

        public static WatchVideo instance;


        public void Awake()
        {
            instance = this;

            m_state = AD_STATE.idle;
            m_responseWait = new WaitForSeconds(m_waitForResponse);
            m_retryWait = new WaitForSeconds(m_retryDelay);

#if UNITY_ANDROID
            m_unitID = m_androidID;
#elif UNITY_IPHONE
            m_unitID = m_iosID;
#else
            m_unitID = "unexpected_platform";
#endif      
            rewardBasedVideo = RewardBasedVideoAd.Instance;
            rewardBasedVideo.OnAdStarted += HandleVideoStarted;
            rewardBasedVideo.OnAdRewarded += HandleVideoRewarded;

            rewardBasedVideo.OnAdLoaded += HandleOnAdLoaded;
            rewardBasedVideo.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            rewardBasedVideo.OnAdClosed += HandleOnAdClosed;
        }

        public static void DispatchReady()
        {
            if (instance.Ready != null)
            {
                instance.Ready();
            }
        }

        public static void DispatchRequest()
        {
            if (instance.Request != null)
            {
                instance.Request();
            }
        }

        public static void DispatchLoaded()
        {
            if (instance.Loaded != null)
            {
                instance.Loaded();
            }
        }

        public static void DispatchStarted()
        {
            if (instance.Started != null)
            {
                instance.Started();
            }
        }

        public static void DispatchClose()
        {
            if (instance.Close != null)
            {
                instance.Close();
            }
        }

        public static void DispatchFail()
        {
            if (instance.Fail != null)
            {
                instance.Fail();
            }
        }

        public static void DispatchReward()
        {
            if (instance.Reward != null)
            {
                instance.Reward();
            }
        }

        public void OnEnable()
        {
            RequestInBackground();
        }

        public void OnDestroy()
        {
            instance = null;
            StopAllCoroutines();
            rewardBasedVideo.OnAdStarted -= HandleVideoStarted;
            rewardBasedVideo.OnAdRewarded -= HandleVideoRewarded;
            rewardBasedVideo.OnAdLoaded -= HandleOnAdLoaded;
            rewardBasedVideo.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            rewardBasedVideo.OnAdClosed -= HandleOnAdClosed;
            display = false;
        }

        void RequestInBackground()
        {
            display = false;
            m_rewarded = false;
            RequestVideo();
        }

        IEnumerator RetryVideo()
        {
            yield return m_retryWait;
            RequestInBackground();
        }

        void RequestVideo()
        {
            adsDB = AdSDB.Get();
            if (adsDB.consent == ADS_CONSENT.relevant)
            {
                request = new AdRequest.Builder().Build();
            }
            else
            {
                request = new AdRequest.Builder().AddExtra("npa", "1").Build();
            }
            rewardBasedVideo.LoadAd(request, m_unitID);
        }

        void ShowVideo()
        {
            display = true;
            switch (m_state)
            {
                case AD_STATE.loaded:
                    if (rewardBasedVideo != null && rewardBasedVideo.IsLoaded())
                    {
                        rewardBasedVideo.Show();
                    }
                    else
                    {
                        RequestVideo();
                    }
                    break;
                case AD_STATE.loading:
                    m_WaitForResponse = StartCoroutine(WaitForResponseStart());
                    break;
                case AD_STATE.failToLoad:
                case AD_STATE.rewarded:
                case AD_STATE.idle:
                    m_WaitForResponse = StartCoroutine(WaitForResponseStart());
                    RequestVideo();
                    break;
            }
        }

        public void OnWatchVideo()
        {
#if UNITY_EDITOR
            if (UnityEngine.Random.Range(0, 3) < 2)
            {
                DispatchReward();
            }
            else
            {
                DispatchFail();
            }
#else
            DispatchRequest();
            m_rewarded = false;
            m_messageDisplayed = false;
            ShowVideo();
#endif
        }

        public static void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            m_state = AD_STATE.failToLoad;
            if (instance !=null && instance.m_WaitForResponse != null)
            {
                instance.StopCoroutine(instance.m_WaitForResponse);
            }

            if (display && instance != null)
            {
                instance.ResponseFail();
            }
            if (instance != null)
            {
                instance.StartCoroutine(instance.RetryVideo());
            }
        }

        public static void HandleOnAdLoaded(object sender, EventArgs args)
        {
            m_state = AD_STATE.loaded;
            if (instance != null && instance.m_WaitForResponse != null)
            {
                instance.StopCoroutine(instance.m_WaitForResponse);
            }
            if (display && instance != null)
            {
                rewardBasedVideo.Show();
                DispatchLoaded();
            }
        }

        public static void HandleVideoStarted(object sender, EventArgs args)
        {
            m_state = AD_STATE.started;
            if (instance != null && instance.m_WaitForResponse != null)
            {
                instance.StopCoroutine(instance.m_WaitForResponse);
            }
            DispatchStarted();
        }

        public static void HandleVideoRewarded(object sender, EventArgs args)
        {
            m_state = AD_STATE.rewarded;
            if (instance != null && instance.m_WaitForResponse != null)
            {
                instance.StopCoroutine(instance.m_WaitForResponse);
            }
            m_rewarded = true;
            if (instance != null)
            {
                instance.Rewarded();
            }
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            m_state = AD_STATE.idle;
            if (m_WaitForResponse != null)
            {
                StopCoroutine(m_WaitForResponse);
            }
            if (display)
            {
                display = false;
                VideoClosed();
            }
            RequestInBackground();
        }

        IEnumerator WaitForResponseStart()
        {
            yield return m_responseWait;
            ResponseFail();
        }

        void ResponseFail()
        {
            display = false;
            DispatchFail();
        }

        void Rewarded()
        {
            if (m_state == AD_STATE.idle && !m_messageDisplayed)
            {
                m_messageDisplayed = true;
                DispatchReward();
            }
        }

        public void VideoClosed()
        {
            if (m_rewarded)
            {
                Rewarded();
            }
            DispatchClose();
        }

        public bool IsVideoReady
        {
            get { return rewardBasedVideo.IsLoaded(); }
        }
    }
}
