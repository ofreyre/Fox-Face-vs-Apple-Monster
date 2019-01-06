using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsDisplay : MonoBehaviour
{
    public LevelsSettings m_settings;
    public GlobalFlow m_flow;
    public SpritesOrderManager m_spriteOrders;
    public float m_marginRight = 1;
    public float m_marginTop = 1;
    public float m_marginBottom = 2;
    public float m_colSpace = 1;
    public float m_minAttackers = 6;

    public void Init ()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        List<GameObject> prefabs = m_settings.settings[m_flow.AbsoluteLevel].hordes.Prefabs;
        Transform ts;
        int n = prefabs.Count;
        int m = n < 6 ? n : 6;
        float dy = (height - m_marginTop - m_marginBottom) / m;
        float y = -(dy * (m - 1)) * 0.5f;
        for (int i = 0; i < n; i++)
        {
            ts = Instantiate(prefabs[i]).transform;
            ts.GetComponent<Animator>().enabled = false;
            ts.position = new Vector3(width * 0.5f - m_marginRight - m_colSpace * (1 - i / m), y + dy * (i % m), 0);
            SpritesOrderManager.instance.SetOrder(i % m, ORDERGROUPTYPE.attackers, ts.GetComponent<Sprites>().m_sprites);
            ts.gameObject.SetActive(true);
        }
    }
}
