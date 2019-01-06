using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGads
{
    public class BtnDisplayUI : MonoBehaviour
    {
        public void OnClick(GameObject ui)
        {
            gameObject.SetActive(false);
            ui.SetActive(true);
        }
    }
}
