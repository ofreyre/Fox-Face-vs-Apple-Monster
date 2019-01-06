using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pool {

    public GameObject m_prefab;
    public int m_min, m_max;
    [HideInInspector]
    public Stack<GameObject> m_objectsOff;
    int m_count;
    GameObject local_gameObject = null;

    public Pool(GameObject prefab, int min, int max)
    {
        m_prefab = prefab;
        m_min = min;
        m_max = max;
        Init();
    }

    public void Init()
    {   
        if (m_objectsOff == null || m_objectsOff.Count < m_min)
        {
            m_prefab.SetActive(false);
            m_objectsOff = new Stack<GameObject>();
            for (int i = m_objectsOff.Count; i < m_min; i++)
            {
                local_gameObject = UnityEngine.Object.Instantiate(m_prefab);
                m_objectsOff.Push(local_gameObject);
                local_gameObject.GetComponent<PoolItem>().m_pool = this;                
            }
            m_count = m_min;
        }
    }

    public GameObject Get() {
        local_gameObject = null;
        int n = m_objectsOff.Count;
        if (n > 0)
        {
            local_gameObject = m_objectsOff.Pop();
        }
        else if(m_count < m_max)
        {
            m_count++;
            local_gameObject = UnityEngine.Object.Instantiate(m_prefab);
            local_gameObject.GetComponent<PoolItem>().m_pool = this;
            
        }
        return local_gameObject;
    }

    public void Return(GameObject gobj) {
        m_objectsOff.Push(gobj);
    }
}
