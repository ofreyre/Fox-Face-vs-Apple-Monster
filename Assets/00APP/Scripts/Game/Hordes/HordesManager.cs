using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordesManager : MonoBehaviour {

    [HideInInspector]
    public Hordes m_hordes;
    int m_hordeI;
    int m_momentI;
    float m_nextT;
    Horde m_horde;
    GameObject[] m_prefabs;
    Transform m_localTransform0;
    GameObject m_localGameObject0;
    Dictionary<ATTACKERTYPE, Pool> m_pools;
    EarnMousetrap m_earnMousetrap;

    public void Init()
    {
        m_horde = m_hordes.hordes[m_hordeI];

        m_pools = new Dictionary<ATTACKERTYPE, Pool>(AttackerType.AttackerTypeComparer);
        Dictionary<ATTACKERTYPE, GameObject> attackerTypes = m_hordes.GetAttackerType();
        Pool pool;
        Animator animator;
        foreach (KeyValuePair<ATTACKERTYPE, GameObject> kv in attackerTypes)
        {
            animator = kv.Value.GetComponent<Animator>();
            if (animator != null)
            {
                animator.keepAnimatorControllerStateOnDisable = true;
            }
            pool = new Pool(kv.Value, 1, 100);
            m_pools.Add(kv.Key, pool);
        }
    }

    public void Begin()
    {
        m_earnMousetrap = EarnMousetrap.instance;
        enabled = true;
        m_nextT = Time.time + m_horde.momments[m_momentI].time;
    }

    public void End()
    {
        enabled = false;
    }

    void Update()
    {
        float t = Time.time;
        if (t >= m_nextT)
        {
            GameAudioPlayer.instance.PlayPlaying();
            int row;
            m_prefabs = m_horde.momments[m_momentI].prefabs;
            Map.instance.ResetHordeRowRND();
            for (int i = 0, n = m_prefabs.Length; i < n; i++)
            {
                if (m_earnMousetrap.m_JforHordes != -1)
                {
                    row = m_earnMousetrap.ConsumeHordesRow();
                }
                else
                {
                    row = Map.instance.GetHordeRowRND();
                }
                //m_localTransform0 = Instantiate(m_prefabs[i]).transform;
                m_localGameObject0 = m_pools[m_prefabs[i].GetComponent<AttackerType>().type].Get();
                SpritesOrderManager.instance.SetOrder(row, ORDERGROUPTYPE.attackers, m_localGameObject0.GetComponent<Sprites>().m_sprites);
                m_localGameObject0.SetActive(true);
                m_localTransform0 = m_localGameObject0.transform;
                m_localTransform0.position = Map.instance.GetHordePositionAtRow(row);
                m_localTransform0.GetComponent<Animator>().Rebind();
                CollisionManager.instance.AddAttacker(row, m_localTransform0);
            }
            if (m_momentI == 0)
            {
                if (m_hordeI == m_hordes.hordes.Length - 1)
                {
                    GameEvents.DispatchFlowEvent(FLOWEVENTTYPE.hordelast);
                }
                else
                {
                    GameEvents.DispatchFlowEvent(FLOWEVENTTYPE.hordestart);
                }
            }

            bool end = false;
            m_momentI++;
            if (m_momentI == m_horde.momments.Length)
            {
                m_hordeI++;
                if (m_hordeI == m_hordes.hordes.Length)
                {
                    end = true;
                }
                else
                {
                    m_momentI = 0;
                    m_horde = m_hordes.hordes[m_hordeI];
                }
            }
            if (!end)
            {
                m_nextT = t + m_horde.momments[m_momentI].time;
            }
            else
            {
                m_nextT = float.PositiveInfinity;
                GameEvents.DispatchFlowEvent(FLOWEVENTTYPE.hordeend);
            }
        }
    }
}
