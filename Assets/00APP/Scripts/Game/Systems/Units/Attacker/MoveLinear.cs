using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLinear : MonoBehaviour, IMove  {

    public float m_speed = -1;
    Vector3 m_velocity;
    float m_freezeTime;
    public float m_protectionIce;
    public float m_protectionGlue;
    public bool moving;
    float m_freeze;
    SpriteRenderer[] m_sprites;
    byte m_freezeType;  //1=ice, 2 = glue
    Animator m_animator;
    Coroutine m_freezeCoroutine;
    float m_k;

    void Awake()
    {
        m_sprites = GetComponent<Sprites>().m_sprites;
        m_animator = GetComponent<Animator>();
        m_k = -1 / 0.191f;
    }

    void OnEnable()
    {
        m_freezeTime = 0;
        m_freezeType = 0;
        m_freeze = 0;
        m_velocity = new Vector3(m_speed, 0, 0);
        m_animator.speed = m_speed * m_k;
        color = Color.white;
    }

    public void Move(float delta)
    {
        transform.position += m_velocity * delta;
    }

    public void Hit(BulletDamage damage)
    {
        if (damage.damageIce > 0)
        {
            Freeze(damage.damageIce, damage.durationIce, m_protectionIce, GameSettings.instance.m_gameConstants.colorIce, 1);
        }
        if (damage.damageGlue > 0)
        {
            Freeze(damage.damageGlue, damage.durationGlue, m_protectionIce, GameSettings.instance.m_gameConstants.colorGlue, 2);
        }
    }

    public void Freeze(float amount, float duration)
    {
        Freeze(amount, duration, m_protectionIce, GameSettings.instance.m_gameConstants.colorIce, 1);
    }

    public void Freeze(float amount, float duration, float protection, Color color, byte freezeType = 1)
    {
        float f = protection * amount;
        if (f > 0)
        {
            if (m_freeze < f)
            {
                m_freeze = f;
                m_velocity.x = m_speed + m_freeze;
                if (m_velocity.x > 0)
                {
                    m_velocity.x = 0;
                }
            }
            float t = Time.time + protection * duration;
            m_freezeTime = (t > m_freezeTime ? t : m_freezeTime);
            if (m_freezeType != freezeType)
            {
                if (m_freezeType == 0)
                {
                    m_freezeCoroutine = StartCoroutine(Freeze());
                }
                m_freezeType = freezeType;
                this.color = color;
            }
            m_animator.speed = m_velocity.x * m_k;
        }
    }

    IEnumerator Freeze()
    {
        while (Time.time < m_freezeTime)
        {
            yield return GameSettings.instance.m_gameConstants.checkLapseFreeze;
        }
        ToNormal();
    }

    Color color
    {
        set
        {
            for (int i = 0, n = m_sprites.Length; i < n; i++)
            {
                m_sprites[i].color = value;
            }
        }
    }

    public float CurrentSpeed { get { return m_velocity.x; } }

    public float Speed
    {
        get { return m_speed; }
        set {
            m_speed = value;
            m_animator.speed = m_speed * m_k;
        }
    }

    public void ToNormal()
    {
        m_freeze = 0;
        m_velocity.x = m_speed;
        m_freezeType = 0;
        color = Color.white;
        m_animator.speed = m_speed * m_k;
        if (m_freezeCoroutine != null)
        {
            StopCoroutine(m_freezeCoroutine);
        }
    }
}
