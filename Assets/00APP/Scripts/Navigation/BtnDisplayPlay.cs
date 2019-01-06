using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDisplayPlay : MonoBehaviour {
    public GlobalFlow m_flow;

	void Start () {
        gameObject.SetActive(m_flow.toPlay);
	}
}
