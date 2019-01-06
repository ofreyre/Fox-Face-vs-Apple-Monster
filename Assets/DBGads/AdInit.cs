using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api.Mediation.AppLovin;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.InMobi;
using GoogleMobileAds.Api.Mediation.MyTarget;

namespace DBGads
{
    public class AdInit : MonoBehaviour
    {
        public string m_nextScene;
        public string m_GDPRscene;
        public string admob_android_appID;
        public string admob_ios_appID;

        // Use this for initialization
        void Start()
        {
            Init();
        }

        void Init()
        {
            AppLovin.Initialize();

            if (AdSDB.Get() != null)
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




                ADS_CONSENT consent = AdSDB.Get().consent;

                /*UnityAds
             * Integrating Unity Ads with Mediation. Scroll to end:
             * https://developers.google.com/admob/unity/mediation/unity
             * 
             */
                UnityAds.SetGDPRConsentMetaData(consent == ADS_CONSENT.relevant ? true : false);

                
                /*InMobi
                 * Integrating InMobi with Mediation. Scroll to end:
                 * https://developers.google.com/admob/unity/mediation/inmobi
                 * 
                 * More information about the possible keys and values that InMobi accepts in this consent object
                 * https://support.inmobi.com/monetize/android-guidelines#h3-null-initializing-the-sdk
                 * 
                 */
                Dictionary<string, string> consentObject = new Dictionary<string, string>();
                consentObject.Add("gdpr_consent_available", consent == ADS_CONSENT.relevant ? "true" : "false");
                consentObject.Add("gdpr", "1");
                InMobi.UpdateGDPRConsent(consentObject);



                AppLovin.SetHasUserConsent(consent == ADS_CONSENT.relevant ? true : false);


                MyTarget.SetUserConsent(consent == ADS_CONSENT.relevant ? true : false);


                GetComponent<Splash>().m_nextScene = m_nextScene;
            }
            else
            {
                GetComponent<Splash>().m_nextScene = m_GDPRscene;
            }
        }
    }
}
