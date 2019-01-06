using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public class ButtonRate : MonoBehaviour
{
    public void OnClick()
    {
        //AudioManager.instance.Play("Many", "button");
#if UNITY_ANDROID
        //Application.OpenURL("market://details?id=com.damnbadgames.sonic.poc.vs.monster.minions");
        Application.OpenURL("market://details?id="+Application.identifier);
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1442690233");
#endif
    }
}
