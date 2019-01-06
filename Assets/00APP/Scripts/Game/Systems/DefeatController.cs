using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public enum SONICSTATE
{
    ready,
    moving,
    disabled,
    ad
}

public class DefeatController : MonoBehaviour {
    public static DefeatController instance;

    public GameObject m_sonicPacmanPrefab;
    public float m_recoverDuration = 30;
    public float m_r;
    public float m_speed = 2;
    public Vector3 m_offset = new Vector3(-0.7f,0,0);
    Transform[] m_lastDefences;
    SONICSTATE[] m_lastDefencesState;
    GameObject m_localGameObject;
    Transform m_localTransform;
    float m_movingLinitX;
    int m_rows;
    float m_t;
    bool recovering;
    int m_selectedRowAd;
    Vector3 m_velocity;
    int m_defencesUsed;
    ClipPlayer[] m_clipPlayers;
    ClipPlayer m_localClipPlayer;
    EarnSonicPacman m_earnSonicPacman;

    // Use this for initialization
    public void Init (EarnSonicPacman earnSonicPacman) {

        instance = this;
        m_earnSonicPacman = earnSonicPacman;
        m_selectedRowAd = -1;
        m_r *= m_r;
        m_velocity = new Vector3(m_speed, 0, 0);
        GameEvents.instance.AttackerArrive += OnAttackerArrive;
        //GameEvents.instance.AttackerEnd += OnAttackerEnd;
        GameEvents.instance.MapClicked += OnMapClicked;

        m_rows = Map.instance.m_cellsY;
        m_lastDefences = new Transform[m_rows];
        m_lastDefencesState = new SONICSTATE[m_rows];
        m_clipPlayers = new ClipPlayer[m_rows];
        for (int i = 0, n = m_rows; i < n; i++)
        {
            m_localGameObject = Instantiate(m_sonicPacmanPrefab);
            m_localGameObject.transform.position = Map.instance.ij2xy(0,i) + m_offset;
            m_lastDefences[i] = m_localGameObject.transform;
            m_localGameObject.GetComponent<SonicPacman>().SetPlay(false);
            m_lastDefencesState[i] = SONICSTATE.ready;
        }
        m_movingLinitX = Map.instance.m_bulletEndX;

    }

    public void Begin()
    {
        enabled = true;
    }

    public void End()
    {
        enabled = false;
        Animator animator;
        for (int i = 0; i < m_rows; i++)
        {
            animator = m_lastDefences[i].GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 0;
            }
            m_localClipPlayer = m_clipPlayers[i];
            if (m_localClipPlayer != null)
            {
                m_localClipPlayer.Stop();
            }
        }
    }

    void Update()
    {
        float delta = Time.deltaTime;
        float x;
        for (int i = 0; i < m_rows; i++)
        {
            if (m_lastDefencesState[i] == SONICSTATE.moving)
            {
                m_localTransform = m_lastDefences[i];
                m_localTransform.position += m_velocity * delta;
                x = m_localTransform.position.x;
                if (x > CollisionManager.instance.GetFirstAttackerLimitX(i))
                {
                    CollisionManager.instance.KillAttacker(0, i);
                }
                if (x > m_movingLinitX)
                {
                    m_localTransform.gameObject.SetActive(false);
                    m_lastDefencesState[i] = SONICSTATE.disabled;
                    m_localClipPlayer = m_clipPlayers[i];
                    if (m_localClipPlayer != null)
                    {
                        m_localClipPlayer.Stop();
                        m_clipPlayers[i] = null;
                    }
                    if (!recovering)
                    {
                        recovering = true;
                        m_t = Time.time + m_recoverDuration;
                    }
                }
            }
        }
        if (recovering && Time.time > m_t)
        {
            FillLastDefence();
            recovering = false;
        }
    }

    void FillLastDefence()
    {
        for (int i = 0; i < m_rows; i++)
        {
            if (m_lastDefencesState[i] == SONICSTATE.disabled)
            {
                m_localGameObject = m_lastDefences[i].gameObject;
                m_localGameObject.transform.position = Map.instance.ij2xy(0, i) + m_offset;
                m_localGameObject.gameObject.SetActive(true);
                m_localGameObject.GetComponent<SonicPacman>().SetPlay(true);
                m_lastDefencesState[i] = SONICSTATE.ad;
            }
        }
    }

    void OnMapClicked(Vector2 screenpos)
    {
        Vector2 pos = Map.instance.Screen2World(screenpos);
        for (int i = 0; i < m_rows; i++)
        {
            if (m_lastDefencesState[i] == SONICSTATE.ad)
            {
                m_localTransform = m_lastDefences[i];
                if ((new Vector2(m_localTransform.position.x, m_localTransform.position.y) - pos).sqrMagnitude < m_r)
                {
                    m_selectedRowAd = i;
                    m_earnSonicPacman.TyToEarn(m_selectedRowAd);
                }
            }
        }
    }

    public void ActivateSonicPacman(int i)
    {
        m_lastDefencesState[i] = SONICSTATE.ready;
        m_localGameObject = m_lastDefences[i].gameObject;
        m_localGameObject.transform.position = Map.instance.ij2xy(0, i) + m_offset;
        m_localGameObject.GetComponent<SonicPacman>().SetPlay(false);
    }

    void OnAttackerArrive(int row)
    {
        if (m_lastDefencesState[row] == SONICSTATE.ready)
        {
            m_lastDefencesState[row] = SONICSTATE.moving;
            m_clipPlayers[row] = GameAudioPlayer.instance.PlaySonicpacman();
            AnimatorController.instance.Move(m_lastDefences[row].GetComponent<Animator>());
            m_defencesUsed += 1;
        }
        else
        {
            GameAudioPlayer.instance.PlayArrive();
        }
    }

    /*void OnAttackerEnd(Transform ts, int row)
    {
        GameEvents.DispatchFlowEvent(FLOWEVENTTYPE.lose);
    }*/

    public int defencesUsed { get { return Mathf.Min(m_defencesUsed, m_rows); } }
}
