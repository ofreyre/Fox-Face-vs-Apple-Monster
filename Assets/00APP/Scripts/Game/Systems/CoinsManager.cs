using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

    public GameObject[] m_coinPrefabs;
    public float[] m_coinProbs;
    public int[] m_coinValues;
    public float m_r = 0.16f;
    Pool[] m_pools;
    List<Transform> m_coinObjects;
    List<int> m_coinsAvailable;
    GameObject m_localGameObject;
    Transform m_localTransform;
    int m_coins = 0;

    GameAudioPlayer m_audioPlayer;

    public void Init (GameAudioPlayer audioPlayer)
    {
        m_audioPlayer = audioPlayer;
        instance = this;
        m_r *= m_r;
        GameEvents.instance.AttackerKilled += OnAttackerKilled;
        GameEvents.instance.MapClicked += OnMapClicked;
        m_pools = new Pool[m_coinPrefabs.Length];
        for (int i = 0, n = m_coinPrefabs.Length; i < n; i++)
        {
            m_pools[i] = new Pool(m_coinPrefabs[i], 10 * (i + 1), 20 * (i + 1));
        }
        m_coinObjects = new List<Transform>();
        m_coinsAvailable = new List<int>();
    }

    public void ApplyUpgrades(int coin0_value, int coin1_value, float coin0_prob, float coin1_prob, float coin_duration)
    {
        m_coinValues[0] += coin0_value;
        m_coinValues[1] += coin1_value;
        m_coinProbs[0] += coin0_prob;
        m_coinProbs[1] += coin1_prob;

        m_coinPrefabs[0].GetComponent<DisableAfterTime>().m_duration += coin_duration;
        m_coinPrefabs[1].GetComponent<DisableAfterTime>().m_duration += coin_duration;
    }

    void OnMapClicked(Vector2 screenpos)
    {
        float a, b;
        Vector2 pos = Map.instance.Screen2World(screenpos);
        int n = m_coinObjects.Count;
        for (int i = 0; i < n; )
        {
            m_localTransform = m_coinObjects[i];

            if (!m_localTransform.gameObject.activeSelf)
            {
                m_coinsAvailable.RemoveAt(i);
                m_coinObjects.RemoveAt(i);
                n--;
            }
            else
            {
                a = pos.x - m_localTransform.position.x;
                b = pos.y - m_localTransform.position.y;
                if (a * a + b * b < m_r)
                {
                    m_audioPlayer.PlayCollectCoin();
                    m_coins += m_coinsAvailable[i];
                    m_localTransform.gameObject.SetActive(false);
                    m_coinsAvailable.RemoveAt(i);
                    m_coinObjects.RemoveAt(i);
                    return;
                }
                i++;
            }
        }
    }

    public void OnAttackerKilled(Vector3 pos, ATTACKERTYPE type)
    {
        float r = Random.Range(0, 1f);
        float a = 0;
        for (int i = 0, n = m_coinProbs.Length; i < n; i++)
        {
            a += m_coinProbs[i];
            if (r < a)
            {
                m_audioPlayer.PlayCoin();
                m_localGameObject = m_pools[i].Get();
                m_localGameObject.SetActive(true);
                m_localGameObject.transform.position = pos;
                m_coinObjects.Add(m_localGameObject.transform);
                m_coinsAvailable.Add(m_coinValues[i]);
                return;
            }
        }
    }

    public int Coins
    {
        get
        {
            return m_coins;
        }
    }

    public void End()
    {
        for (int i = 0, n = m_coinsAvailable.Count; i < n; i++)
        {
            m_coins += m_coinsAvailable[i];
            m_coinObjects[i].gameObject.SetActive(false);
        }
    }
}
