using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGPhysics2D;

public class ElipticBullet : MonoBehaviour, IMove {    
    public float m_speed = 1;
    float m_g;
    Vector3 m_velocity;
    float t;

    public void Impulse(Vector3 p1, float speed)
    {
        m_g = DBGPhysics2D.Physics2D.g;
        m_velocity = Bullet2D.GetImpulseToTarget(transform.position, p1, m_speed, speed, m_g);
    }
	
	public void Move (float delta)
    {
        t += Time.deltaTime;
        transform.position += m_velocity * delta;
        m_velocity.y += m_g * delta;
    }

    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }


}
