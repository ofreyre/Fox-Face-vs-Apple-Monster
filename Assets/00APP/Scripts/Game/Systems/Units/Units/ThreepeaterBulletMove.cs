using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreepeaterBulletMove : MonoBehaviour {
    public float m_speed = 2;
    public BULLETTYPE m_nextBulletType;
    Vector3 m_velocity;
    float m_arriveY;

    public void Init(int row, Vector3 direction, float arriveY)
    {
        m_velocity = direction * m_speed / direction.x;
        m_arriveY = arriveY;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += m_velocity * Time.deltaTime;
        if ( (m_velocity.y > 0 && transform.position.y > m_arriveY) || (m_velocity.y < 0 && transform.position.y < m_arriveY))
        {
            UnitsSpawner.instance.Shoot(m_nextBulletType, transform.position);
            gameObject.SetActive(false);
        }
    }
}
