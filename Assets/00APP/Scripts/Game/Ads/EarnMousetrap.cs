using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;

public class EarnMousetrap : MonoBehaviour {

    public static EarnMousetrap instance;

    public float m_spawnFirstTime = 3;
    public float m_spawnTime = 60;
    public float m_spawnRetrytime = 5;
    public GameObject m_prefab;
    public int iMin = 5, iMax=7, jMin=0, jMax = 5;
    public float m_fallSpeed = -2;
    WaitForSeconds m_spawnWait;
    WaitForSeconds m_spawnWaitRetry;
    bool m_active;
    GameObject m_spawned;
    float m_y0;
    [HideInInspector]
    public Vector3 m_arrivePos;
    Animator m_animator;
    int m_j;
    [HideInInspector]
    public int m_JforHordes;
    Vector2 m_gridOffset;
    StateOnClick m_stateBehaviour;

    // Use this for initialization
    public void Init () {
        instance = this;
        m_j = -1;
        m_JforHordes = -1;
        m_spawnWait = new WaitForSeconds(m_spawnTime);
        m_spawnWaitRetry = new WaitForSeconds(m_spawnRetrytime);
        m_y0 = Camera.main.transform.position.y + Camera.main.orthographicSize;
        m_spawned = Instantiate(m_prefab);
        m_animator = m_spawned.GetComponent<Animator>();
    }

    public void Begin()
    {
        StartCoroutine(TrySpawnFirstTime());
    }

    public void End()
    {
        MousetrapEnd();
        StopAllCoroutines();
        enabled = false;
    }

    IEnumerator TrySpawnFirstTime()
    {
        yield return new WaitForSeconds(m_spawnFirstTime);
        while (!WatchVideo.instance.IsVideoReady)
        {
            yield return m_spawnWaitRetry;
        }
        Spawn();
    }

    IEnumerator TrySpawn()
    {
        yield return m_spawnWait;
        while (!WatchVideo.instance.IsVideoReady)
        {
            yield return m_spawnWaitRetry;
        }
        Spawn();
    }

    void Spawn()
    {
        int i = Random.Range(iMin, iMax + 1);
        m_j = Random.Range(jMin, jMax + 1);
        m_arrivePos = Map.instance.ij2xy(i, m_j);
        Vector3 v0 = new Vector3(m_arrivePos.x, m_y0,  0);
        m_spawned.transform.position = v0;
        m_spawned.SetActive(true);
    }

    void ClearVideoListeners()
    {
        WatchVideo.instance.Close -= OnClose;
        WatchVideo.instance.Fail -= OnFail;
        WatchVideo.instance.Reward -= OnReward;
    }

    void ClearTextMessageListeners()
    {
        EventManagerMessages.instance.Remove -= OnRemoveBeforeWatch;
        EventManagerMessages.instance.Watch -= OnWatchVideo;
        EventManagerMessages.instance.No -= OnDontRemove;
        EventManagerMessages.instance.Yes -= OnRemove;
        EventManagerMessages.instance.OK -= OnEarned;
    }

    public void OnMousetrapClick(StateOnClick stateBehaviour)
    {
        m_stateBehaviour = stateBehaviour;
        Time.timeScale = 0;
        ClearTextMessageListeners();
        EventManagerMessages.instance.Remove += OnRemoveBeforeWatch;
        EventManagerMessages.instance.Watch += OnWatchVideo;
        EventManagerMessages.instance.DispatchMessage("Do you want to remove the mousetrap or watch a video to activate it?", false, false, false, true, true);
    }

    void OnRemoveBeforeWatch()
    {
        OnRemove();
    }

    void OnWatchVideo()
    {
        ClearTextMessageListeners();
        ClearVideoListeners();
        WatchVideo.instance.Close += OnClose;
        WatchVideo.instance.Fail += OnFail;
        WatchVideo.instance.Reward += OnReward;
        WatchVideo.instance.OnWatchVideo();
    }

    public void OnClose()
    {
        ClearVideoListeners();
        ClearTextMessageListeners();
        EventManagerMessages.instance.No += OnDontRemove;
        EventManagerMessages.instance.Yes += OnRemove;
        EventManagerMessages.instance.DispatchMessage("Do you want to remove the mousetrap?", false, true, true);
    }

    public void OnFail()
    {
        ClearVideoListeners();
        m_stateBehaviour.enabled = true;
        ClearTextMessageListeners();
        EventManagerMessages.instance.No += OnDontRemove;
        EventManagerMessages.instance.Yes += OnRemove;
        EventManagerMessages.instance.DispatchMessage("Sorry, no video available.\nYou can try again later.\nDo you want to remove the mousetrap?", false, true, true);
    }

    public void OnReward()
    {
        ClearVideoListeners();
        m_stateBehaviour.enabled = false;
        m_JforHordes = m_j;
        ClearTextMessageListeners();
        EventManagerMessages.instance.OK += OnEarned;
        EventManagerMessages.instance.DispatchMessage("You earned a mousetrap!", true, false, false);
    }

    public void OnDontRemove()
    {
        ClearTextMessageListeners();
        Time.timeScale = 1;
        m_stateBehaviour.enabled = true;
    }

    public void OnRemove()
    {
        ClearTextMessageListeners();
        Time.timeScale = 1;
        m_JforHordes = -1;
        MousetrapEnd();
    }

    public void OnEarned()
    {
        ClearTextMessageListeners();
        Time.timeScale = 1;
        AnimatorController.instance.Aim(m_animator);
    }

    public void MousetrapEnd()
    {
        m_spawned.SetActive(false);
        StartCoroutine(TrySpawn());
    }

    public int ConsumeHordesRow()
    {
        int j = m_JforHordes;
        m_JforHordes = -1;
        return j;
    }
}
