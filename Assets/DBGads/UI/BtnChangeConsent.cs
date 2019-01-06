using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.InMobi;
using GoogleMobileAds.Api.Mediation.AppLovin;
using GoogleMobileAds.Api.Mediation.MyTarget;
using GoogleMobileAds.Api;

namespace DBGads
{
    public class BtnChangeConsent : MonoBehaviour
    {
        public GlobalFlow m_flow;
        public string m_nextScene;
        public string admob_android_appID;
        public string admob_ios_appID;


        public void SetConsent(bool consent)
        {

            //Initialize Mediator 
#if UNITY_ANDROID
            string appId = admob_android_appID;
#elif UNITY_IPHONE
            string appId = admob_ios_appID;
#else
            string appId = "unexpected_platform";
#endif
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);


            /*UnityAds
             * Integrating Unity Ads with Mediation. Scroll to end:
             * https://developers.google.com/admob/unity/mediation/unity
             * 
             */
            UnityAds.SetGDPRConsentMetaData(consent);


            /*InMobi
             * Integrating InMobi with Mediation. Scroll to end:
             * https://developers.google.com/admob/unity/mediation/inmobi
             * 
             * More information about the possible keys and values that InMobi accepts in this consent object
             * https://support.inmobi.com/monetize/android-guidelines#h3-null-initializing-the-sdk
             * 
             */
            Dictionary<string, string> consentObject = new Dictionary<string, string>();
            consentObject.Add("gdpr_consent_available", consent ? "true" : "false");
            consentObject.Add("gdpr", "1");
            InMobi.UpdateGDPRConsent(consentObject);
            AdSDB.Save(consent? ADS_CONSENT.relevant: ADS_CONSENT.random);



            AppLovin.SetHasUserConsent(consent);


            MyTarget.SetUserConsent(consent);


            m_flow.ToScene(m_nextScene);
        }
    }
}
