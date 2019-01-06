using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreepeaterSpawner : MonoBehaviour {
    public static ThreepeaterSpawner instance;

    public Vector3 m_directionUp;
    Map m_map;
    UnitsSpawner m_unitSpawner;
    GameObject m_localGameObject;
    ThreepeaterBulletMove m_move;

    public void Init()
    {
        instance = this;
        m_unitSpawner = UnitsSpawner.instance;
        m_directionUp = m_directionUp.normalized;
        m_map = Map.instance;
    }


    public void Shoot(BULLETTYPE bulletType, Vector3 position)
    {
        int row = m_map.y2j(position.y);
        BULLETTYPE nextBulletType = BULLETTYPE.none;
        if (row > 0)
        {
            nextBulletType = Spawn(m_unitSpawner.m_bulletPools[bulletType], position, new Vector3(m_directionUp.x, -m_directionUp.y, 0), row - 1, position.y - m_map.m_cellL.y);
        }
        if (row < m_map.m_cellsY - 1)
        {
            nextBulletType = Spawn(m_unitSpawner.m_bulletPools[bulletType], position, m_directionUp, row + 1, position.y + m_map.m_cellL.y);
        }
        m_unitSpawner.Shoot(nextBulletType, position);
    }

    BULLETTYPE Spawn(Pool pool, Vector3 position, Vector3 direction, int row, float arriveY)
    {
        m_localGameObject = pool.Get();
        m_move = m_localGameObject.GetComponent<ThreepeaterBulletMove>();
        m_move.Init(row, direction, arriveY);
        m_localGameObject.transform.position = position;
        SpritesOrderManager.instance.SetOrder(row, ORDERGROUPTYPE.bullets, m_localGameObject.GetComponent<Sprites>().m_sprites);
        m_localGameObject.SetActive(true);
        return m_move.m_nextBulletType;
    }
}
