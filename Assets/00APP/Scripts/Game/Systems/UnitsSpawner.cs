using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct BulletSpaceData
{
    public Vector2 position;
    public float r2;
    public GameObject gameObject;
    public int coins;

    public BulletSpaceData(Vector2 position, float radius2, GameObject gameObject, int coins)
    {
        this.position = position;
        this.r2 = radius2;
        this.gameObject = gameObject;
        this.coins = coins;
    }
}

public class UnitsSpawner : MonoBehaviour {
    public static UnitsSpawner instance;

    public UnitsBank m_bank;
    public MissPacmanBank m_bankMissPacman;
    public AnimatorStatesSpeedBank m_bankSpeeds;
    public ExplosionRangeBank m_bankRange;
    public BulletsBank m_bulletsBank;
    public ExplosionsBank m_explosionsBank;
    public float m_hitK = 1;

    Dictionary<UNITTYPE, Pool> m_unitPools;
    public Dictionary<BULLETTYPE, Pool> m_bulletPools;
    public Dictionary<UNITTYPE, BULLETTYPE> m_unitBullet;
    public Dictionary<BULLETTYPE, BulletDamage> m_bulletDamages;
    Dictionary<BULLETTYPE, EXPLOSIONTYPE> m_bulletExplosion;
    Dictionary<EXPLOSIONTYPE, Pool> m_explosionPool;
    Dictionary<UNITTYPE, MissPacmanBankItem> m_missPacmanBank;
    Dictionary<UNITTYPE, Dictionary<AnimatorController.ANIMATORSTATES, float>> m_speedsBank;
    Dictionary<UNITTYPE, ExplosionRangeBankItem> m_rangeBank;
    Dictionary<UNITTYPE, Dictionary<UNITTYPE, bool>> m_unitFill;
    GameObject m_localGameObject;
    Transform m_localTransform;
    List<BulletSpaceData> m_bulletSpaceData;
    Vector4 m_mapRect;
    PacmanBabyData m_localPacmanBabyData;
    Dictionary<UNITTYPE, bool> m_localAdditive;
    ElipticBullet m_elliptic;

    GameAudioPlayer m_audioPlayer;

    // Use this for initialization
    public void Init (GameAudioPlayer audioPlayer)
    {
        m_audioPlayer = audioPlayer;
        GameEvents.instance.MapClicked += OnMapClicked;

        instance = this;
        //m_mapRect = Map.instance.mapRect;
        m_mapRect = Map.instance.mapRectWorld;
        UNITTYPE[] types = GameInventory.instance.m_types;
        m_unitPools = new Dictionary<UNITTYPE, Pool>(UnitType.UnitTypeComparer);
        m_bulletPools = new Dictionary<BULLETTYPE, Pool>(UnitType.BulletTypeComparer);
        m_unitBullet = new Dictionary<UNITTYPE, BULLETTYPE>(UnitType.UnitTypeComparer);
        m_bulletDamages = new Dictionary<BULLETTYPE, BulletDamage>(UnitType.BulletTypeComparer);
        m_explosionPool = new Dictionary<EXPLOSIONTYPE, Pool>(UnitType.ExplosionTypeComparer);
        m_bulletExplosion = new Dictionary<BULLETTYPE, EXPLOSIONTYPE>(UnitType.BulletTypeComparer);
        m_unitFill = new Dictionary<UNITTYPE, Dictionary<UNITTYPE, bool>>(UnitType.UnitTypeComparer);

        m_missPacmanBank = new Dictionary<UNITTYPE, MissPacmanBankItem>(UnitType.UnitTypeComparer);
        m_speedsBank = new Dictionary<UNITTYPE, Dictionary<AnimatorController.ANIMATORSTATES, float>>(UnitType.UnitTypeComparer);
        m_rangeBank = new Dictionary<UNITTYPE, ExplosionRangeBankItem>(UnitType.UnitTypeComparer);

        UNITTYPE type;
        BULLETTYPE bulletType;
        Pool pool;
        UnitsBankItem bankUnit;
        MissPacmanBankItem missPacmanUnit;
        AnimatorUnitStatesSpeedBankItem items;
        AnimatorStatesSpeedBankItem item;
        Dictionary<AnimatorController.ANIMATORSTATES, float> speeds;
        ExplosionRangeBankItem rangeItem;
        ThreepeaterBulletMove threepeaterBulletMove;

        for (int i = 0, n = types.Length; i < n; i++)
        {
            type = types[i];
            bankUnit = m_bank.UNITTYPE_2_Item(type);
            m_localAdditive = new Dictionary<UNITTYPE, bool>(UnitType.UnitTypeComparer);
            for (int k = 0; k < bankUnit.cellFill.Length; k++)
            {
                m_localAdditive.Add(bankUnit.cellFill[k].type, bankUnit.cellFill[k].additive);
            }
            m_unitFill.Add(type, m_localAdditive);

            pool = new Pool(m_bank.UNITTYPE_2_GameObject(type), 5, Map.instance.cellsCount);
            pool.m_prefab = m_bank.UNITTYPE_2_GameObject(type);
            m_unitPools.Add(type, pool);
            bulletType = bankUnit.bulletType;
            m_localGameObject = AddBullet(type, bulletType);
            if (m_localGameObject != null)
            {
                threepeaterBulletMove = m_localGameObject.GetComponent<ThreepeaterBulletMove>();
                if (threepeaterBulletMove != null)
                {
                    AddBullet(UNITTYPE.none, threepeaterBulletMove.m_nextBulletType);
                }
            }


            missPacmanUnit = m_bankMissPacman.GetItem(type);
            if (missPacmanUnit.type != UNITTYPE.none)
            {
                m_missPacmanBank.Add(missPacmanUnit.type, missPacmanUnit);
            }

            items = m_bankSpeeds.GetItem(type);
            if (items.type != UNITTYPE.none)
            {
                speeds = new Dictionary<AnimatorController.ANIMATORSTATES, float>(AnimatorController.AnimatorStatesComparer);
                for (int j = 0, m = items.items.Length; j < m; j++)
                {
                    item = items.items[j];
                    speeds.Add(item.state, item.duration);
                }
                m_speedsBank.Add(items.type, speeds);
            }

            rangeItem = m_bankRange.GetItem(type);
            if (rangeItem.type != UNITTYPE.none)
            {
                m_rangeBank.Add(rangeItem.type, rangeItem);
            }
        }

        if (!m_rangeBank.ContainsKey(UNITTYPE.mousetrap))
        {
            m_rangeBank.Add(UNITTYPE.mousetrap, m_bankRange.GetItem(UNITTYPE.mousetrap));
        }

        m_bulletSpaceData = new List<BulletSpaceData>();
    }

    public void End()
    {
        UnitType[] unitTypes = FindObjectsOfType<UnitType>();
        UNITTYPE type;
        AnimatorWrapper animatorwrapper;
        if (unitTypes != null)
        {
            for (int i = 0, n = unitTypes.Length; i < n; i++)
            {
                type = unitTypes[i].type;
                if (type == UNITTYPE.sunflower_one || type == UNITTYPE.sunflower_two || type == UNITTYPE.sunflower_three)
                {
                    animatorwrapper = unitTypes[i].GetComponent<AnimatorWrapper>();
                    if (animatorwrapper != null)
                    {
                        animatorwrapper.speed = 0;
                    }
                }
            }
        }
    }

    GameObject AddBullet(UNITTYPE type, BULLETTYPE bulletType)
    {
        if (type != UNITTYPE.none)
        {
            m_unitBullet.Add(type, bulletType);
        }
        if (bulletType != BULLETTYPE.none && !m_bulletPools.ContainsKey(bulletType))
        {
            BulletBankItem bullet = m_bulletsBank.BULLETTYPE_2_Item(bulletType);
            Pool pool = new Pool(bullet.prefab, 20, Map.instance.cellsCount * 18);
            m_bulletPools.Add(bulletType, pool);
            m_bulletDamages.Add(bulletType, bullet.damage);
            if (bullet.explosionType != EXPLOSIONTYPE.none)
            {
                m_bulletExplosion.Add(bulletType, bullet.explosionType);
                if (bullet.explosionType != EXPLOSIONTYPE.none && !m_explosionPool.ContainsKey(bullet.explosionType))
                {
                    m_explosionPool.Add(bullet.explosionType,
                        new Pool(m_explosionsBank.BULLETTYPE_2_GameObject(bullet.explosionType), 5, Map.instance.cellsCount)
                    );
                }
            }
            return pool.m_prefab;
        }
        return null;
    }

    public GameObject AddFreeBullet(BULLETTYPE bulletType)
    {
        if (!m_bulletPools.ContainsKey(bulletType))
        {
            BulletBankItem bullet = m_bulletsBank.BULLETTYPE_2_Item(bulletType);
            Pool pool = new Pool(bullet.prefab, 15, Map.instance.cellsCount * 3);
            m_bulletPools.Add(bulletType, pool);
            m_bulletDamages.Add(bulletType, bullet.damage);
            m_bulletExplosion.Add(bulletType, bullet.explosionType);
            if (bullet.explosionType != EXPLOSIONTYPE.none && !m_explosionPool.ContainsKey(bullet.explosionType))
            {
                pool = new Pool(m_explosionsBank.BULLETTYPE_2_GameObject(bullet.explosionType), 5, Map.instance.cellsCount);
                m_explosionPool.Add(bullet.explosionType, pool);
            }
            return pool.m_prefab;
        }
        return null;
    }

    public void ApplyUpgrades(float stamina, float protectionHit, float attackHit, float attackice, float attackfire, float attackair, float ammoSpeed, float ammoRange, int babypacman_value, float babypacman_duration, float characterspeed_animator, float characterspeed_reload)
    {
        ShootData shootData;
        foreach (KeyValuePair<UNITTYPE, Pool> kv in m_unitPools)
        {
            foreach (GameObject gobj in kv.Value.m_objectsOff)
            {
                gobj.GetComponent<UnitState>().staminaMax += stamina + protectionHit;
                shootData = gobj.GetComponent<ShootData>();
                if (shootData != null)
                {
                    shootData.reloadDuration -= characterspeed_reload;
                    shootData.animatorSpeed += characterspeed_animator;

                }
            }
        }

        Dictionary<BULLETTYPE, BulletDamage> bulletDamages = new Dictionary<BULLETTYPE, BulletDamage>(UnitType.BulletTypeComparer);
        foreach (KeyValuePair<BULLETTYPE, BulletDamage> kv in m_bulletDamages)
        {
            BulletDamage bd = new BulletDamage(
                    kv.Value.damageHit + (kv.Value.damageHit > 0 ? attackHit : 0),
                    kv.Value.damageFire + (kv.Value.damageFire > 0 ? attackfire : 0),
                    kv.Value.damageIce + (kv.Value.damageIce > 0 ? attackice : 0),
                    kv.Value.damageAir + (kv.Value.damageAir > 0 ? attackair : 0),
                    kv.Value.damageGlue,
                    kv.Value.durationIce,
                    kv.Value.durationGlue
                );
            bulletDamages.Add(kv.Key, bd);
        }
        m_bulletDamages = bulletDamages;

        IMove move;
        ColliderLinear collider;
        foreach (KeyValuePair<BULLETTYPE, Pool> kv in m_bulletPools)
        {
            foreach (GameObject gobj in kv.Value.m_objectsOff)
            {
                move = gobj.GetComponent<IMove>();
                if (move != null)
                {
                    move.Speed += ammoSpeed;
                }
                collider = gobj.GetComponent<ColliderLinear>();
                if (collider != null)
                {
                    collider.ramge += ammoRange;
                }
            }
        }

        Dictionary<UNITTYPE, MissPacmanBankItem> missPacmanBank = new Dictionary<UNITTYPE, MissPacmanBankItem>(UnitType.UnitTypeComparer);
        foreach (KeyValuePair<UNITTYPE, MissPacmanBankItem> kv in m_missPacmanBank)
        {
            missPacmanBank.Add(kv.Key, new MissPacmanBankItem(kv.Value.type, kv.Value.timeFirstDeliver, kv.Value.timeDeliver - babypacman_duration, kv.Value.babies + babypacman_value));
        }
        m_missPacmanBank = missPacmanBank;

        Dictionary<UNITTYPE, ExplosionRangeBankItem> rangeBank = new Dictionary<UNITTYPE, ExplosionRangeBankItem>(UnitType.UnitTypeComparer);
        foreach (KeyValuePair<UNITTYPE, ExplosionRangeBankItem> kv in m_rangeBank)
        {
            rangeBank.Add(kv.Key, new ExplosionRangeBankItem(kv.Value.type, kv.Value.rowRange, kv.Value.columnRangeMin, kv.Value.columnRangeMax, kv.Value.columnDetectionMin, kv.Value.columnDetectionMax,
                new BulletDamage(kv.Value.damage.damageHit + attackHit, kv.Value.damage.damageFire + attackfire, kv.Value.damage.damageIce + attackice, kv.Value.damage.damageAir + attackair, kv.Value.damage.damageGlue, kv.Value.damage.durationIce, kv.Value.damage.durationGlue)));
        }
        m_rangeBank = rangeBank;
    }

    bool CollectBullets(Vector2 v)
    {
        BulletSpaceData data;
        float a, b;
        int n = m_bulletSpaceData.Count;
        for (int i = 0; i < n;)
        {
            data = m_bulletSpaceData[i];
            if (!data.gameObject.activeSelf)
            {
                m_bulletSpaceData.RemoveAt(i);
                n--;
            }
            else
            {
                a = v.x - data.position.x;
                b = v.y - data.position.y;
                if (a * a + b * b < data.r2)
                {
                    m_audioPlayer.PlayCollectBabypacman();
                    data.gameObject.SetActive(false);
                    m_bulletSpaceData.RemoveAt(i);
                    GameInventory.instance.coins += data.coins;
                    return true;
                }
                i++;
            }
        }
        return false;
    }

    public void OnMapClicked(Vector2 screenpos)
    {
        Vector2 pos = Map.instance.Screen2World(screenpos);
        if (!CollectBullets(pos))
        {
            UNITTYPE type = GameInventory.instance.m_inventoryType;
            if (type != UNITTYPE.none)
            {
                if (m_mapRect.x < pos.x && pos.x < m_mapRect.z && m_mapRect.y < pos.y && pos.y < m_mapRect.w)
                {
                    Vector2Int grid = Map.instance.World2Grid(pos);
                    UNITTYPE fillType = Map.instance.x2filled(grid.x, grid.y);
                    m_localAdditive = m_unitFill[type];
                    if (m_localAdditive.ContainsKey(fillType))
                    {
                        if (fillType != UNITTYPE.none && !m_localAdditive[fillType])
                        {
                            Map.instance.RemoveUnitImmediate(grid.x, grid.y);
                        }
                        m_localGameObject = m_unitPools[type].Get();
                        SpritesOrderManager.instance.SetOrder(grid.y, ORDERGROUPTYPE.units, m_localGameObject.GetComponent<Sprites>().m_sprites);
                        m_localGameObject.GetComponent<Animator>().Rebind();
                        m_localGameObject.SetActive(true);
                        Map.instance.AddUnit(grid, m_localGameObject);
                    }
                }
            }
        }
    }

    public void Shoot(UNITTYPE type, Vector3 position)
    {
        Shoot(m_unitBullet[type], position);
    }

    public void Shoot(BULLETTYPE bulletType, Vector3 position)
    {
        m_audioPlayer.PlayShoot();
        m_localGameObject = m_bulletPools[bulletType].Get();
        if (m_localGameObject != null)
        {
            m_localGameObject.transform.position = position;
            int row = Map.instance.y2j(m_localGameObject.transform.position.y);
            SpritesOrderManager.instance.SetOrder(row, ORDERGROUPTYPE.bullets, m_localGameObject.GetComponent<Sprites>().m_sprites);
            m_localGameObject.SetActive(true);
            CollisionManager.instance.AddBullet(m_localGameObject.transform, m_bulletDamages[bulletType], bulletType);
        }
    }

    public void ShootEliptic(UNITTYPE type, Vector3 p0, Vector3 p1, float speed1)
    {
        m_audioPlayer.PlayShootEliptic();
        BULLETTYPE bulletType = m_unitBullet[type];
        m_localGameObject = m_bulletPools[bulletType].Get();
        m_localGameObject.transform.position = p0;
        m_elliptic = m_localGameObject.GetComponent<ElipticBullet>();
        m_elliptic.Impulse(p1, speed1);
        //int row = Map.instance.y2j(m_localGameObject.transform.position.y);
        int row = Map.instance.y2j(p0.y);
        SpritesOrderManager.instance.SetOrder(row, ORDERGROUPTYPE.bullets, m_localGameObject.GetComponent<Sprites>().m_sprites);
        m_localGameObject.SetActive(true);
        CollisionManager.instance.AddElliptic(m_localGameObject.transform, m_bulletDamages[bulletType], bulletType);
    }

    public void ShootRange(UNITTYPE type, Vector3 p0)
    {
        m_audioPlayer.PlayExplosionLong();
        Vector2Int grid = Map.instance.World2Grid(p0);
        ExplosionRangeBankItem range = GetExplosionRange(type);
        BULLETTYPE bulletType = m_unitBullet[type];
        for (int i = (int)Mathf.Max(0, grid.x + range.columnRangeMin), n = (int)Math.Min(Map.instance.m_cellsX, grid.x + range.columnRangeMax + 1); i < n; i++)
        {
            m_localGameObject = m_bulletPools[bulletType].Get();
            m_localGameObject.transform.position = new Vector3(Map.instance.i2x(i), p0.y, 0);
            SpritesOrderManager.instance.SetOrder(grid.y, ORDERGROUPTYPE.bullets, m_localGameObject.GetComponent<Sprites>().m_sprites);
            m_localGameObject.SetActive(true);
        }
    }

    public void Explode(BULLETTYPE type, Vector3 position, int row)
    {
        m_localGameObject = m_explosionPool[m_bulletExplosion[type]].Get();
        m_localGameObject.transform.position = position;
        //int row = Map.instance.y2j(m_localGameObject.transform.position.y);
        SpritesOrderManager.instance.SetOrder(row, ORDERGROUPTYPE.bullets, m_localGameObject.GetComponent<Sprites>().m_sprites);
        m_localGameObject.SetActive(true);
    }

    public BulletDamage GetBulletDamage(BULLETTYPE type)
    {
        return m_bulletDamages[type];
    }

    public void SpawnBullet(UNITTYPE type, Vector3 position)
    {
        m_audioPlayer.PlaySpawnBabypacman();
        m_localGameObject = m_bulletPools[m_unitBullet[type]].Get();
        m_localGameObject.transform.position = position;
        m_localGameObject.SetActive(true);
        m_localPacmanBabyData = m_localGameObject.GetComponent<PacmanBabyData>();
        m_bulletSpaceData.Add(new BulletSpaceData(new Vector2(position.x, position.y), m_localPacmanBabyData.r2, m_localGameObject, m_localPacmanBabyData.coins));
    }

    public BulletSpaceData SpawnBulletFree(BULLETTYPE type, Vector3 position)
    {
        m_audioPlayer.PlaySpawnBabypacman();
        m_localGameObject = m_bulletPools[type].Get();
        m_localGameObject.transform.position = position;
        m_localGameObject.SetActive(true);
        m_localPacmanBabyData = m_localGameObject.GetComponent<PacmanBabyData>();
        return new BulletSpaceData(new Vector2(position.x, position.y), m_localPacmanBabyData.r2, m_localGameObject, m_localPacmanBabyData.coins);
    }

    public MissPacmanBankItem GetMissPacmanBankItem(UNITTYPE type)
    {
        return m_missPacmanBank[type];
    }

    public float GetStateDuration(UNITTYPE type, AnimatorController.ANIMATORSTATES state)
    {
        return m_speedsBank[type][state];
    }

    public ExplosionRangeBankItem GetExplosionRange(UNITTYPE type)
    {
        return m_rangeBank[type];
    }
}
