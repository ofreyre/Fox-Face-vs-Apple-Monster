using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOpenWeb : MonoBehaviour {
    public void OnClick(string url)
    {
        Application.OpenURL(url);
    }
}
