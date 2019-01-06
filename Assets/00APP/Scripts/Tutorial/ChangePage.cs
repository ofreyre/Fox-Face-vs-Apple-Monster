using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour {

    public GameObject m_nextPage;

    public void OnClick()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        m_nextPage.SetActive(true);
    }
}
