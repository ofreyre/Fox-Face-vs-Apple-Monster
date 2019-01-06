using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPacmanSpawner : MonoBehaviour
{
    public static BabyPacmanSpawner instance;

    BabypacmanSettings m_settings;
    float m_xMin, m_xMax, m_y0, m_yMin, m_yMax;
    float m_t_spawn;
    Vector3 m_velocity;
    float m_t0;
    List<BulletSpaceData> m_spacepacmanData = new List<BulletSpaceData>();
    List<Transform> m_babypacman = new List<Transform>();

    public float m_lapseStart = 10;
    public float m_lapseMin = 15;
    public float m_lapseIncPerSecond = 0.01f;

    Transform m_localTransform;
    int m_babypacmanCount;

    // Use this for initialization
    public void Init(LevelSettings settings)
    {
        instance = this;

        //GameEvents.instance.MapClicked += OnMapClicked;
        m_settings = settings.babypacman;

        m_lapseStart = m_settings.lapseStart;
        m_lapseMin = m_settings.lapseMin;
        m_lapseIncPerSecond = m_settings.lapseIncPerSecond;

        Vector4 rect = Map.instance.mapRectWorld;
        m_xMin = rect.x + m_settings.marginLeft;
        m_xMax = rect.z + m_settings.marginRight;
        m_yMin = rect.y + m_settings.marginBottom;
        m_yMax = rect.w + m_settings.marginTop;
        Camera cam = Camera.main;
        m_y0 = cam.transform.position.y + cam.orthographicSize;
        m_velocity = m_settings.velocity;

        UnitsSpawner.instance.AddFreeBullet(BULLETTYPE.babypacman);
    }

    public void End()
    {
        enabled = false;
    }

    public void ApplyUpgrades(float amount)
    {
        m_lapseMin -= amount * 10;
    }

    public void Begin()
    {
        m_t0 = Time.time;
        m_t_spawn = m_t0 + m_lapseStart;
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        BulletSpaceData data;
        if (t > m_t_spawn)
        {
            data = UnitsSpawner.instance.SpawnBulletFree(BULLETTYPE.babypacman, new Vector3(Random.Range(m_xMin, m_xMax), m_y0, 1));
            data.position = new Vector2(0, Random.Range(m_yMin, m_yMax));
            m_spacepacmanData.Add(data);
            m_babypacman.Add(data.gameObject.transform);
            m_t_spawn = m_t_spawn + m_lapseMin + (t - m_t0) * m_lapseIncPerSecond;
            m_babypacmanCount += 1;
        }

        t = Time.deltaTime;
        for (int i = 0; i < m_babypacmanCount; )
        {
            data = m_spacepacmanData[i];
            m_localTransform = m_babypacman[i];
            if (data.gameObject.activeSelf)
            {
                if (m_localTransform.position.y > data.position.y)
                {
                    m_localTransform.position += m_velocity * t;
                }
                i++;
            }
            else
            {
                m_spacepacmanData.RemoveAt(i);
                m_babypacman.RemoveAt(i);
                m_babypacmanCount--;
            }
        }
    }


    public bool OnMapClicked(Vector2 screenpos)
    {
        Vector2 pos = Map.instance.Screen2World(screenpos);
        float a, b;
        BulletSpaceData data;
        Vector3 pacmanPos;
        for (int i = 0; i < m_babypacmanCount; i++)
        {
            data = m_spacepacmanData[i];
            pacmanPos = m_babypacman[i].position;
            a = pos.x - pacmanPos.x;
            b = pos.y - pacmanPos.y;
            if (a * a + b * b < data.r2)
            {
                GameAudioPlayer.instance.PlayCollectBabypacman();
                GameInventory.instance.coins += data.coins;
                m_spacepacmanData.RemoveAt(i);
                m_babypacman.RemoveAt(i);
                data.gameObject.SetActive(false);
                m_babypacmanCount--;
                return true;
            }
        }
        return false;
    }
}
