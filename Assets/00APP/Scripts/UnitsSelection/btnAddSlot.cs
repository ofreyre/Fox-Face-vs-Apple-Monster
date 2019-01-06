using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DBGads;

public class btnAddSlot : MonoBehaviour
{
    SelectedUnitsController m_selectedUnitsController;
    int m_i;

    public void Init (int i, SelectedUnitsController selectedUnitsController)
    {
        WatchVideo.instance.Request += OnRequest;
        WatchVideo.instance.Fail += OnFail;
        WatchVideo.instance.Reward += OnReward;
        WatchVideo.instance.Reward += OnClose;
        m_i = i;
        m_selectedUnitsController = selectedUnitsController;
        GetComponent<Button>().onClick.AddListener(() => WatchVideo.instance.OnWatchVideo());
    }

    void OnRequest()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    void OnReward()
    {
        StopListening();
        m_selectedUnitsController.AddSlot(m_i);
        Destroy(gameObject);
    }

    private void OnFail()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    private void OnClose()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    void StopListening()
    {
        WatchVideo.instance.Request -= OnRequest;
        WatchVideo.instance.Fail -= OnFail;
        WatchVideo.instance.Reward -= OnReward;
        WatchVideo.instance.Reward -= OnClose;
    }
}
