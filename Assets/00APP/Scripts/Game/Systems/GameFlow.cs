using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManagement;

public class GameFlow : MonoBehaviour {

    public GlobalFlow m_flow;
    public GameStats m_stats;
    public GameObject m_messageStart;
    public GameObject m_messageFlow;
    public string m_messageHordeStart;
    public string m_messageHordeLast;
    public string m_messageWin;
    public string m_messageLose;
    public string m_endGameScene;
    public float m_endDuration = 5;
    public float m_checkEndLapse = 1;
    public OptionsManager m_options;
    Coroutine m_checkNoMoreAttackers;
    Coroutine m_checkWin;
    WaitForSeconds m_lapse;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        GameEvents.instance.FlowEvent += OnFlowEvent;
        m_options.Resume += OnResume;
        m_options.Exit += OnExit;
        m_options.Restart += OnRestart;

        GetComponent<GameSettings>().Init();
        m_lapse = new WaitForSeconds(m_checkEndLapse);

        Begin();
    }

    public void Begin()
    {
        GameAudioPlayer.instance.PlayStart();
        m_messageStart.SetActive(true);
    }

    void OnFlowEvent(FLOWEVENTTYPE type)
    {
        switch (type)
        {
            case FLOWEVENTTYPE.start:
                System.GC.Collect();
                CollisionManager.instance.Begin();
                gameObject.GetComponent<HordesManager>().Begin();
                gameObject.GetComponent<DefeatController>().Begin();
                gameObject.GetComponent<MouseInput>().Begin();
                gameObject.GetComponent<BabyPacmanSpawner>().Begin();
                gameObject.GetComponent<EarnMousetrap>().Begin();
                break;

            case FLOWEVENTTYPE.hordestart:
                m_messageFlow.GetComponent<Text>().text = m_messageHordeStart;
                m_messageFlow.SetActive(true);
                if (m_checkNoMoreAttackers == null)
                {
                    m_checkNoMoreAttackers = StartCoroutine(CheckNoMoreAttackers());
                }
                break;

            case FLOWEVENTTYPE.hordelast:
                m_messageFlow.GetComponent<Text>().text = m_messageHordeLast;
                m_messageFlow.SetActive(true);
                if (m_checkNoMoreAttackers == null)
                {
                    m_checkNoMoreAttackers = StartCoroutine(CheckNoMoreAttackers());
                }
                break;

            case FLOWEVENTTYPE.hordeend:
                if (m_checkNoMoreAttackers != null)
                {
                    StopCoroutine(m_checkNoMoreAttackers);
                }
                m_checkWin = StartCoroutine(CheckWin());
                break;
            case FLOWEVENTTYPE.lose:
                if (m_checkWin != null)
                {
                    StopCoroutine(m_checkWin);
                }
                GameAudioPlayer.instance.StopPlaying();
                GameAudioPlayer.instance.PlayLose();
                End(false);
                m_messageFlow.GetComponent<Text>().text = m_messageLose;
                break;
        }
    }

    IEnumerator CheckNoMoreAttackers()
    {
        while (CollisionManager.instance.existAttackers)
        {
            yield return m_lapse;
        }
        GameAudioPlayer.instance.StopPlaying();
        m_checkNoMoreAttackers = null;
    }

    IEnumerator CheckWin()
    {
        while (CollisionManager.instance.existAttackers)
        {
            yield return m_lapse;
        }
        GameAudioPlayer.instance.StopPlaying();
        GameAudioPlayer.instance.PlayWin();
        End(true);
        m_messageFlow.GetComponent<Text>().text = m_messageWin;
    }

    void End(bool win)
    {
        GameEvents.instance.FlowEvent -= OnFlowEvent;
        //CollisionManager.instance.End();
        gameObject.GetComponent<MouseInput>().End();
        gameObject.GetComponent<DefeatController>().End();
        gameObject.GetComponent<HordesManager>().End();
        gameObject.GetComponent<UnitsSpawner>().End();
        gameObject.GetComponent<CoinsManager>().End();
        gameObject.GetComponent<BabyPacmanSpawner>().End();
        gameObject.GetComponent<EarnMousetrap>().End();
        Destroy(GameObject.Find("EventSystem"));
        m_messageFlow.GetComponent<TextBlink>().enabled = false;
        m_messageFlow.GetComponent<InactiveAfterTime>().enabled = false;
        m_messageFlow.SetActive(true);
        m_stats.Score = ScoreManager.instance.m_score;
        m_stats.babyPacmans = GameInventory.instance.m_coins;
        m_stats.Coins = CoinsManager.instance.Coins;
        m_stats.SetStars(Map.instance.m_cellsY, win ? DefeatController.instance.defencesUsed : -1);
        StartCoroutine(LoadEndGame(win));
    }

    IEnumerator LoadEndGame(bool win)
    {
        yield return new WaitForSeconds(m_endDuration);
        m_flow.ToScene(m_endGameScene);
    }

    public void OnOptions()
    {
        Time.timeScale = 0;
        m_options.Display(true);
    }

    void OnExit()
    {
        Time.timeScale = 1;
        m_flow.ToScene("Main");
    }

    void OnResume()
    {
        Time.timeScale = 1;
    }

    void OnRestart()
    {
        Time.timeScale = 1;
        m_flow.ToScene("ItemsStore");
    }
}
