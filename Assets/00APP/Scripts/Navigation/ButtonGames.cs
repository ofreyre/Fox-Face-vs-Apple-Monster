using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public class ButtonGames : MonoBehaviour {
    public void OnClick()
    {
        //AudioManager.instance.Play("Many", "button");
#if UNITY_ANDROID
        try
        {
            Application.OpenURL("https://play.google.com/store/apps/dev?id=8002457556097517242");
            //Application.OpenURL("market://dev?id=8002457556097517242");
        }
        catch
        {
            Application.OpenURL("https://play.google.com/store/apps/dev?id=8002457556097517242");
        }
#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://appstore.com/oscarfreyre");
#endif
    }
}
