using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnGamepediaAttacker : MonoBehaviour {
    public float m_thumbnailScale = 0.3f;
    public Transform m_thumbnailContainer;
    ATTACKERTYPE type;
    
	public void Fill (GamepediaAttackerBankItem item) {
        type = item.type;
        GameObject gobj = Instantiate(item.prefab);
        gobj.transform.localScale = new Vector3(m_thumbnailScale, m_thumbnailScale, m_thumbnailScale);
        gobj.transform.SetParent(m_thumbnailContainer);
    }

    public void OnClick()
    {
        GamepediaEvents.DispatchAttackerSelected(type);
    }
}
