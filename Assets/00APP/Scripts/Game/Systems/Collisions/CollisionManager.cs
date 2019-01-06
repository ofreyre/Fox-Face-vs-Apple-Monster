using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATTACKERSTATE
{
    move,
    attack,
    arrive,
    end,
}

public class Attacker
{
    public Transform transform;
    public MoveLinear move;
    public AttackerLife life;
    public float limitX;
    public float limitY;
    public float range;
    public ATTACKERSTATE state;
    public int row;

    public Attacker(Transform transform, MoveLinear move, AttackerLife life, ColliderLinear collider, float range, ATTACKERSTATE state, int row)
    {
        this.transform = transform;
        this.move = move;
        this.life = life;
        limitX = collider.limitX;
        limitY = collider.limitY;
        this.range = range;
        this.state = state;
        this.row = row;
    }
}

public class Attack
{
    public AttackerAttack attack;
    public int i, j;
    //public UnitState unitState;

    //public Attack(AttackerAttack attack, UnitState unitState, int i, int j)
    public Attack(AttackerAttack attack, int i, int j)
    {
        this.attack = attack;
        //this.unitState = unitState;
        this.i = i;
        this.j = j;
    }
}

public class Bullet
{
    public Transform transform;
    public IMove move;
    public float limitX;
    public float limitY;
    public float range;
    public BULLETTYPE type;
    public int row;

    public Bullet(Transform transform, IMove move, ColliderLinear collider, BULLETTYPE type, int row)
    {
        this.transform = transform;
        this.move = move;
        limitX = collider.limitX;
        limitY = collider.limitY;
        range = collider.ramge;
        this.type = type;
        this.row = row;
    }
}

public class CollisionManager : MonoBehaviour {
    public static CollisionManager instance;

    public UnitsBank m_bank;
    public float m_checkLapse = 0.5f;
    [HideInInspector]
    public MapSettings m_settings;

    public List<List<Attacker>> m_attackers;
    public List<List<Attack>> m_attack;
    public List<List<Bullet>> m_bullets;
    public List<List<Bullet>> m_elliptics;

    float m_attackerArriveX;
    float m_attackerEndX;
    float m_bulletEndX;

    List<Attacker> m_localAttackerRow;
    List<Attack> m_localAttackRow;
    List<Bullet> m_localBulletRow;
    Attacker m_localAttacker, m_localAttacker1;
    Attack m_localAttack, m_localAttack1;
    Bullet m_localBullet, m_localBullet1;

    GameAudioPlayer m_audioPlayer;

    public void Init(GameAudioPlayer audioPlayer)
    {
        instance = this;
        m_audioPlayer = audioPlayer;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        m_attackerArriveX = Map.instance.m_origin.x;
        m_attackerEndX = Map.instance.m_attackerEndX;
        m_bulletEndX = cam.transform.position.x + width * 0.5f;
        
        m_attackers = new List<List<Attacker>>();
        m_attack = new List<List<Attack>>();
        m_bullets = new List<List<Bullet>>();
        m_elliptics = new List<List<Bullet>>();

        for (int i = 0, n = m_settings.cellsY; i < n; i++)
        {
            m_attackers.Add(new List<Attacker>());
            m_attack.Add(new List<Attack>());
            m_bullets.Add(new List<Bullet>());
            m_elliptics.Add(new List<Bullet>());
        }
    }

    public void Begin()
    {
        enabled = true;
    }

    public void End()
    {
        enabled = false;
    }

    #region Attackers

    public void AddAttacker(int row, Transform ts)
    {
        AttackerAttack attack = ts.GetComponent<AttackerAttack>();
        m_attackers[row].Add(new Attacker(ts, ts.GetComponent<MoveLinear>(),
            ts.GetComponent<AttackerLife>(),
            ts.GetComponent<ColliderLinear>(),
            attack.m_range,
            ATTACKERSTATE.move, row));
        //m_attack[row].Add(new Attack(attack, null, -1, -1));
        m_attack[row].Add(new Attack(attack, -1, -1));
    }

    public void MoveAttackers(float delta)
    {
        int I;
        float x;
        for (int j = 0, n = m_attackers.Count; j < n; j++)
        {
            m_localAttackerRow = m_attackers[j];
            m_localAttackRow = m_attack[j];
            for (int i = 0, m = m_localAttackerRow.Count; i < m;)
            {
                m_localAttacker = m_localAttackerRow[i];
                switch (m_localAttacker.state)
                {
                    case ATTACKERSTATE.move:
                        m_localAttacker.move.Move(delta);
                        x = m_localAttacker.transform.position.x + m_localAttacker.range;
                        I = Map.instance.x2i(x);
                        //if (Map.instance.x2filled(I, j) != UNITTYPE.none && )
                        if(Map.instance.GetUnitLimit(I, j) > x)
                        {
                            m_localAttack = m_localAttackRow[i];
                            //m_localAttack.unitState = Map.instance.m_defenders[I,j].transform.GetComponent<UnitState>();
                            m_localAttack.i = I;
                            m_localAttack.j = j;
                            m_localAttacker.state = ATTACKERSTATE.attack;
                            m_localAttack.attack.Begin(I,j);
                        }
                        else if (m_localAttacker.transform.position.x < m_attackerArriveX)
                        {
                            m_localAttacker.state = ATTACKERSTATE.arrive;
                            GameEvents.DispatchAttackerArrive(j);
                        }
                        i++;
                        break;
                    case ATTACKERSTATE.attack:
                        m_localAttack = m_localAttackRow[i];
                        //if (m_localAttack.unitState.m_state == UNITSTATE.dead || !Map.instance.x2filled(m_localAttack.i, m_localAttack.j))
                        //if (Map.instance.x2filled(m_localAttack.i, m_localAttack.j) == UNITTYPE.none)
                        {
                            if (m_localAttacker.move.moving)
                            {
                                m_localAttacker.state = ATTACKERSTATE.move;
                            }
                        }
                        i++;
                        break;
                    case ATTACKERSTATE.arrive:
                        m_localAttacker.move.Move(delta);
                        if (m_localAttacker.transform.position.x > m_attackerEndX)
                        {
                            i++;
                        }
                        else
                        {
                            EndAttacker(i,j);
                            m--;
                        }
                        break;
                }
            }
        }
    }

    public void SortAttackers()
    {
        int m, i, k;
        float x;
        for (int j = 0, n = m_attackers.Count; j < n; j++)
        {
            m_localAttackerRow = m_attackers[j];
            m_localAttackRow = m_attack[j];
            m = m_localAttackerRow.Count;
            if (m > 1)
            {
                i = 1;
                k = 1;
                while (i < m)
                {
                    m_localAttacker = m_localAttackerRow[i];
                    x = m_localAttacker.transform.position.x + m_localAttacker.limitX;
                    while (true)
                    {
                        m_localAttacker1 = m_localAttackerRow[k - 1];
                        if (x < m_localAttacker1.transform.position.x + m_localAttacker1.limitX)
                        {
                            m_localAttackerRow[k] = m_localAttacker1;
                            m_localAttackerRow[k - 1] = m_localAttacker;
                            m_localAttack = m_localAttackRow[k];
                            m_localAttackRow[k] = m_localAttackRow[k - 1];
                            m_localAttackRow[k - 1] = m_localAttack;

                            k--;
                            if (k == 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    i++;
                    k = i;
                }
            }
        }
    }

    public void RemoveAttacker(int i, int j)
    {
        m_attackers[j].RemoveAt(i);
        m_attack[j].RemoveAt(i);
    }

    public void RemoveAttacker(Transform ts)
    {
        int j = Map.instance.y2j(ts.position.y);
        m_localAttackerRow = m_attackers[j];
        for (int i = 0, n = m_localAttackerRow.Count; i < n; i++)
        {
            if (m_localAttackerRow[i].transform == ts)
            {
                RemoveAttacker(i, j);
                break;
            }
        }
    }

    public void KillAttacker(int i, int j)
    {
        m_attackers[j][i].life.Die();
        RemoveAttacker(i, j);
    }

    public void EndAttacker(int i, int j)
    {
        m_attackers[j][i].transform.gameObject.SetActive(false);
        GameEvents.DispatchFlowEvent(FLOWEVENTTYPE.lose);
        RemoveAttacker(i, j);
    }

    int GetNearAttacker(int j, float x, float limiX, bool isElliptic = false, float y = 0, float limitY = 0)
    {
        //Debug.Log("GetNearAttacker "+j + " " + x + " " + limiX + " " + isElliptic + " " + y + " " + limitY);
        m_localAttackerRow = m_attackers[j];
        int i = 0, c = m_localAttackerRow.Count - 1, m;
        float x1;
        while (i <= c)
        {
            m = i + (c - i) / 2;
            m_localAttacker = m_localAttackerRow[m];
            x1 = m_localAttacker.transform.position.x;
            if (x + limiX > x1 + m_localAttacker.limitX && x - limiX < x1 - m_localAttacker.limitX)
            //if (x > x1 + m_localAttacker.limitX && x - limiX < x1 - m_localAttacker.limitX)
            {
                if (!isElliptic)
                {
                    return m;
                }
                if (y - limitY < m_localAttacker.transform.position.y + m_localAttacker.limitY)
                {
                    return m;
                }
            }
            
            if (x1 < x)
                i = m + 1;
            
            else
                c = m - 1;
        }
        return -1;
    }

    public float GetFirstAttackerLimitX(int row)
    {
        m_localAttackerRow = m_attackers[row];
        if (m_localAttackerRow.Count > 0)
        {
            m_localAttacker = m_localAttackerRow[0];
            return m_localAttacker.transform.position.x + m_localAttacker.limitX;
        }
        return 999999;
    }

    public bool ExistRightAttacker(Vector3 pos, float r)
    {
        m_localAttackerRow = m_attackers[Map.instance.y2j(pos.y)];
        int n = m_localAttackerRow.Count;
        if (n > 0)
        {
            float x = m_localAttackerRow[n - 1].transform.position.x;
            return n > 0 && x > pos.x;
        }
        return false;
    }

    public bool ExistRightAttacker(int row, float posX, float r)
    {
        m_localAttackerRow = m_attackers[row];
        int n = m_localAttackerRow.Count;
        return n > 0 && m_localAttackerRow[n - 1].transform.position.x > posX;
    }

    public Attacker GetFirstRightAttacker(Vector3 unitPos, float bulletX)
    {
        Vector2Int grid = Map.instance.World2Grid(unitPos);
        m_localAttackerRow = m_attackers[grid.y];
        int n = m_localAttackerRow.Count - 1;
        int low = 0, high = n, mid;
        float lowX, highX, midX;
        //Debug.Log(grid.y+" "+n);
        while (low <= high)
        {
            lowX = m_localAttackerRow[low].transform.position.x;
            //Debug.Log("bulletX = "+ bulletX+ "   lowX = " + lowX);
            if (bulletX <= lowX)
            {
                return m_localAttackerRow[low];
            }

            highX = m_localAttackerRow[high].transform.position.x;
            if (bulletX > highX)
            {
                return null;
            }

            mid = (low + high) / 2;
            midX = m_localAttackerRow[mid].transform.position.x;


            /*if (midX == bulletX)
            {
                if (mid < n)
                {
                    return m_localAttackerRow[mid + 1];
                }
                else
                {
                    return null;
                }
            }
            else if (midX < bulletX)
            {
                if (mid + 1 <= high && bulletX < m_localAttackerRow[mid + 1].transform.position.x)
                {
                    return m_localAttackerRow[mid + 1];
                }
                else
                {
                    low = mid + 1;
                }
            }
            else
            {
                if (mid - 1 >= low && bulletX > m_localAttackerRow[mid - 1].transform.position.x)
                {
                    if (midX > bulletX)
                    {
                        return m_localAttackerRow[mid];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    high = mid - 1;
                }
            }*/

            if (bulletX <= midX)
            {
                if (mid - 1 >= low && bulletX > m_localAttackerRow[mid - 1].transform.position.x)
                {
                    return m_localAttackerRow[mid];
                }
                else
                {
                    high = mid - 1;
                }
            }
            else //if (bulletX > midX)
            {
                if (mid + 1 <= high && bulletX < m_localAttackerRow[mid + 1].transform.position.x)
                {
                    return m_localAttackerRow[mid + 1];
                }
                else
                {
                    low = mid + 1;
                }
            }
        }
        return null;
    }

    public bool ExistAttackerAtRightInBulletRange(Vector3 unitPos, float bulletX)
    {
        Attacker attacker = GetFirstRightAttacker(unitPos, bulletX);
        if (attacker == null)
        {
            return false;
        }
        return attacker.transform.position.x < m_bulletEndX;
    }

    public void DamageAttackers(float bulletX, float bulletLimitX, int attackerI, int attackerJ, BulletDamage damage, bool isElliptic = false, float bulletY = 0, float bulletLimitY = 0)
    {
        float x1 = bulletX - bulletLimitX;
        float y1 = bulletY - bulletLimitY;
        m_localAttackerRow = m_attackers[attackerJ];
        int i = attackerI;
        m_localAttacker = m_localAttackerRow[i];
        float x = m_localAttacker.transform.position.x + m_localAttacker.limitX;
        do
        {
            if (!isElliptic || y1 < m_localAttacker.transform.position.y + m_localAttacker.limitY)
            {
                m_localAttacker.move.Hit(damage);
                if (!m_localAttacker.life.Hit(damage))
                {
                    m_audioPlayer.PlayMinionDie();
                    RemoveAttacker(i, attackerJ);
                    GameEvents.DispatchAttackerKilled(m_localAttacker.transform.position, m_localAttacker.transform.GetComponent<AttackerType>().type);
                    attackerI--;
                }
            }
            i--;
            if (i < 0)
            {
                break;
            }
            m_localAttacker = m_localAttackerRow[i];
            x = m_localAttacker.transform.position.x + m_localAttacker.limitX;

        } while (x1 < x);

        x1 = bulletX + bulletLimitX;
        attackerI++;
        int n = m_localAttackerRow.Count;
        if (attackerI < n)
        {
            n--;
            i = attackerI;
            m_localAttacker = m_localAttackerRow[i];
            x = m_localAttacker.transform.position.x - m_localAttacker.limitX;
            if (x1 > x)
            {
                do
                {
                    if (!isElliptic || y1 < m_localAttacker.transform.position.y + m_localAttacker.limitY)
                    {
                        m_localAttacker.move.Hit(damage);
                        if (!m_localAttacker.life.Hit(damage))
                        {
                            m_audioPlayer.PlayMinionDie();
                            RemoveAttacker(i, attackerJ);
                            GameEvents.DispatchAttackerKilled(m_localAttacker.transform.position, m_localAttacker.transform.GetComponent<AttackerType>().type);
                            n--;
                        }
                    }
                    else
                    {
                        i++;
                    }
                    if (i > n)
                    {
                        break;
                    }
                    m_localAttacker = m_localAttackerRow[i];
                    x = m_localAttacker.transform.position.x - m_localAttacker.limitX;

                } while (x1 > x);
            }
        }
    }

    public void DamageAttackersRange(Vector3 worldPos, UNITTYPE type)
    {
        ExplosionRangeBankItem range = UnitsSpawner.instance.GetExplosionRange(type);
        int j0 = Map.instance.y2j(worldPos.y);
        int attackerI;
        float x0 = Map.instance.i2mapX(range.columnRangeMin);
        float x1 = Map.instance.i2mapX(range.columnRangeMax);
        x0 = worldPos.x + x0 > m_attackerArriveX ? x0 : m_attackerArriveX - worldPos.x;
        x1 = worldPos.x + x1 < m_bulletEndX ? x1 : m_bulletEndX - worldPos.x;
        float r = (x1 - x0) * 0.5f;
        float x = worldPos.x +(x1 + x0) * 0.5f;
        
        for (int j = (j0 - range.rowRange > -1 ? j0 - range.rowRange : 0),
            n = (j0 + range.rowRange + 1 < Map.instance.m_cellsY + 1 ? j0 + range.rowRange + 1 : Map.instance.m_cellsY); j < n; j++)
        {
            attackerI = GetNearAttacker(j, x, r);
            if (attackerI != -1)
            {
                DamageAttackers(x, r, attackerI, j, range.damage);
            }
        }
    }

    public Vector2 GetColRangeWorld(Vector3 worldPos, UNITTYPE type)
    {
        ExplosionRangeBankItem range = UnitsSpawner.instance.GetExplosionRange(type);
        //float x0 = Map.instance.i2mapX(range.columnRangeMin);
        //float x1 = Map.instance.i2mapX(range.columnRangeMax);

        float x0 = Map.instance.i2mapX(range.columnDetectionMin);
        float x1 = Map.instance.i2mapX(range.columnDetectionMax);
        x0 = worldPos.x + x0 > m_attackerArriveX ? x0 : m_attackerArriveX - worldPos.x;
        x1 = worldPos.x + x1 < m_bulletEndX ? x1 : m_bulletEndX - worldPos.x;
        return new Vector2(worldPos.x + (x1 + x0) * 0.5f, (x1 - x0) * 0.5f);
    }

    public Vector2Int GetRowRange(Vector3 worldPos, UNITTYPE type)
    {
        ExplosionRangeBankItem range = UnitsSpawner.instance.GetExplosionRange(type);
        int j0 = Map.instance.y2j(worldPos.y);
        return new Vector2Int(j0 - range.rowRange > -1 ? j0 - range.rowRange : 0, j0 + range.rowRange + 1 < Map.instance.m_cellsY + 1 ? j0 + range.rowRange + 1 : Map.instance.m_cellsY);
    }

    public bool IsAttackerInRange(int j0, int j1, float x, float r)
    {
        for (int j = j0; j < j1; j++)
        {
            if (GetNearAttacker(j, x, r) != -1)
            {
                return true;
            }
        }
        return false;
    }

    public bool existAttackers
    {
        get
        {
            for (int i = 0, n = Map.instance.m_cellsY; i < n; i++)
            {
                if (m_attackers[i].Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }

    #endregion

    #region Bullets
    public void AddBullet(Transform ts, BulletDamage damage, BULLETTYPE type)
    {
        int row = Map.instance.y2j(ts.position.y);
        m_bullets[row].Add(new Bullet(ts, ts.GetComponent<MoveBullet>(), ts.GetComponent<ColliderLinear>(), type, row));
    }

    public void MoveBullets(List<List<Bullet>> bullets, float delta)
    {
        for (int j = 0, n = m_bullets.Count; j < n; j++)
        {
            m_localBulletRow = bullets[j];
            float limitY = Map.instance.GetRowSpawnY(j);
            for (int i = 0, m = m_localBulletRow.Count; i < m;)
            {
                m_localBullet = m_localBulletRow[i];
                m_localBullet.move.Move(delta);
                if (m_localBullet.transform.position.x < m_bulletEndX && m_localBullet.transform.position.y - m_localBullet.limitY > limitY)
                {
                    i++;
                }
                else
                {
                    RemoveBullet(bullets, i, j, false);
                    m--;
                }
            }
        }
    }

    public void RemoveBullet(List<List<Bullet>> bullets, int i, int j, bool explode)
    {
        m_localBullet = bullets[j][i];
        bullets[j].RemoveAt(i);
        m_localBullet.transform.gameObject.SetActive(false);
        if (explode)
        {
            UnitsSpawner.instance.Explode(m_localBullet.type, m_localBullet.transform.position, j);
        }
    }

    public void RemoveBullet(int i, int j, bool explode)
    {
        m_localBullet = m_bullets[j][i];
        m_bullets[j].RemoveAt(i);
        m_localBullet.transform.gameObject.SetActive(false);
        if (explode)
        {
            UnitsSpawner.instance.Explode(m_localBullet.type, m_localBullet.transform.position, j);
        }
    }

    public void BulletsCollisions()
    {
        int attackerI;
        for (int j = 0, n = m_bullets.Count; j < n; j++)
        {
            m_localBulletRow = m_bullets[j];
            int m = m_localBulletRow.Count;
            for (int i = 0; i < m;)
            {   
                m_localBullet = m_localBulletRow[i];
                attackerI = GetNearAttacker(j, m_localBullet.transform.position.x, m_localBullet.limitX);
                if (attackerI != -1)
                {
                    DamageAttackers(m_localBullet.transform.position.x, m_localBullet.range, attackerI, j, UnitsSpawner.instance.GetBulletDamage(m_localBullet.type));
                    RemoveBullet(i, j, true);
                    m--;
                }
                else
                {
                    i++;
                }
            }
        }
    }
    #endregion Bullets

    #region Elliptics
    public void AddElliptic(Transform ts, BulletDamage damage, BULLETTYPE type)
    {
        int row = Map.instance.y2j(ts.position.y);
        m_elliptics[row].Add(new Bullet(ts, ts.GetComponent<ElipticBullet>(), ts.GetComponent<ColliderLinear>(), type, row));
    }

    public void EllipticsCollisions()
    {
        int attackerI;
        for (int j = 0, n = m_bullets.Count; j < n; j++)
        {
            m_localBulletRow = m_elliptics[j];
            int m = m_localBulletRow.Count;
            for (int i = 0; i < m;)
            {
                m_localBullet = m_localBulletRow[i];
                attackerI = GetNearAttacker(j, m_localBullet.transform.position.x, m_localBullet.limitX, true, m_localBullet.transform.position.y, m_localBullet.limitY);
                if (attackerI != -1)
                {
                    DamageAttackers(m_localBullet.transform.position.x, m_localBullet.range, attackerI, j, UnitsSpawner.instance.GetBulletDamage(m_localBullet.type), true, m_localBullet.transform.position.y, m_localBullet.range);
                    RemoveBullet(m_elliptics, i, j, true);
                    m--;
                }
                else
                {
                    i++;
                }
            }
        }
    }
    #endregion Elliptics

    #region Apply items
    public void ApplyIce(float amount, float duration)
    {
        for (int j = 0, n = m_attackers.Count; j < n; j++)
        {
            m_localAttackerRow = m_attackers[j];
            for (int i = 0, m = m_localAttackerRow.Count; i < m; i++)
            {
                m_localAttackerRow[i].move.Freeze(amount, duration);
            }
        }
    }

    public void ApplyDamage(BulletDamage damage)
    {
        for (int j = 0, n = m_attackers.Count; j < n; j++)
        {
            m_localAttackerRow = m_attackers[j];
            for (int i = 0, m = m_localAttackerRow.Count; i < m;)
            {
                m_localAttacker = m_localAttackerRow[i];
                if (!m_localAttacker.life.Hit(damage))
                {
                    m_audioPlayer.PlayMinionDie();
                    RemoveAttacker(i, j);
                    GameEvents.DispatchAttackerKilled(m_localAttacker.transform.position, m_localAttacker.transform.GetComponent<AttackerType>().type);
                    m--;
                }
                else
                {
                    i++;
                }
            }
        }
    }
    #endregion Apply items


    void Update()
    {
        float t = Time.deltaTime;
        MoveAttackers(t);
        MoveBullets(m_bullets, t);
        MoveBullets(m_elliptics,t);
        SortAttackers();
        BulletsCollisions();
        EllipticsCollisions();
    }

}
