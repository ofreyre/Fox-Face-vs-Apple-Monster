using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGads
{
    public class BtnGDPRinfo : MonoBehaviour
    {
        public void OnClick(string m_url)
        {
            Application.OpenURL(m_url);
        }
    }
}
