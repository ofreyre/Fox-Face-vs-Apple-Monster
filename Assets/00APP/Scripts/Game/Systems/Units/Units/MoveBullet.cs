using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour, IMove
{
    public float m_speed = 1;
    Vector3 m_velocity;

    void Awake()
    {
        m_velocity = new Vector3(m_speed, 0, 0);
    }

    public void Move(float delta)
    {
        transform.position += m_velocity * delta;
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }
}
