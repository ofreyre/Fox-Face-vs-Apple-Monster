using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerLife : MonoBehaviour
{
    public AttackerStaminaData[] m_protectionData;
    public GameObject[] m_destructible;
    public Transform m_life;
    float m_totalLifeMax;
    float m_totalLife;
    int m_receiver;
    AttackerStaminaData m_protection;
    float m_stamina;
    Animator m_animator;
    Vector3 m_lifeScale;
    MoveLinear m_move;

    private void Awake()
    {
        m_move = GetComponent<MoveLinear>();
        m_animator = GetComponent<Animator>();
        m_totalLifeMax = 0;
        m_lifeScale = m_life.localScale;
        for (int i = 0, n = m_protectionData.Length; i < n; i++)
        {
            m_totalLifeMax += m_protectionData[i].stamina;
        }
    }

    private void OnEnable()
    {
        m_totalLife = m_totalLifeMax;
        m_life.localScale = m_lifeScale;
        m_receiver = m_protectionData.Length - 1;
        m_protection = m_protectionData[m_receiver];
        m_stamina = m_protection.stamina;
        for (int i = 0, n = m_destructible.Length; i<n; i++)
        {
            m_destructible[i].SetActive(true);
        }
    }

    public bool Hit(BulletDamage damage)
    {
        float d = m_protection.protectionHit * damage.damageHit
            + m_protection.protectionFire * damage.damageFire + m_protection.protectionAir * damage.damageAir;
        m_totalLife -= d;
        m_stamina -= d;
        float scale = m_lifeScale.x * m_totalLife / m_totalLifeMax;
        m_life.localScale = new Vector3(scale < 0?0:scale, m_lifeScale.y, m_lifeScale.z);
        if (m_stamina <= 0)
        {
            if (m_receiver > 0)
            {
                m_destructible[m_receiver - 1].SetActive(false);
                m_receiver--;
                m_protection = m_protectionData[m_receiver];
                m_stamina += m_protection.stamina;
                if (m_stamina > 0)
                {
                    return true;
                }
                else
                {
                    m_move.ToNormal();
                    AnimatorController.instance.Die(m_animator);
                    return false;
                }
            }
            else
            {
                m_move.ToNormal();
                AnimatorController.instance.Die(m_animator);
                return false;
            }
        }
        return true;
    }

    public void Die()
    {
        m_move.ToNormal();
        AnimatorController.instance.Die(m_animator);
    }
}
