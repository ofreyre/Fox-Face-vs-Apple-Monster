using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBGads;

public class EarnSonicPacman : MonoBehaviour {

    int m_sonicPacmanI;
    DefeatController m_defeatcontroller;

    public void Init(DefeatController defeatcontroller)
    {
        m_defeatcontroller = defeatcontroller;
    }

    public void TyToEarn(int sonicPacmanI)
    {
        Time.timeScale = 0;
        m_sonicPacmanI = sonicPacmanI;
        ClearTextMessageListeners();
        EventManagerMessages.instance.No += OnMessageDontWatch;
        EventManagerMessages.instance.Yes += OnMessageWatch;
        EventManagerMessages.instance.DispatchMessage("Watch a video to activate the Sonic Pacman and save your defenders from a humiliating defeat!", false, true, true);
    }

    void ClearTextMessageListeners()
    {
        EventManagerMessages.instance.No -= OnMessageDontWatch;
        EventManagerMessages.instance.Yes -= OnMessageWatch;
        EventManagerMessages.instance.OK -= OnMessageFailed;
        EventManagerMessages.instance.OK -= OnMessageEarned;
    }

    void OnMessageDontWatch()
    {
        ClearTextMessageListeners();
        Time.timeScale = 1;
    }

    void OnMessageWatch()
    {
        ClearTextMessageListeners();
        OnWatchVideo();
    }

    public void OnMessageFailed()
    {
        ClearTextMessageListeners();
        Time.timeScale = 1;
    }

    public void OnMessageEarned()
    {
        ClearTextMessageListeners();
        Time.timeScale = 1;
    }


    #region Video

    void OnWatchVideo()
    {
        ClearTextMessageListeners();
        ClearVideoListeners();
        WatchVideo.instance.Close += OnVideoClose;
        WatchVideo.instance.Fail += OnVideoFail;
        WatchVideo.instance.Reward += OnVideoReward;
        WatchVideo.instance.OnWatchVideo();
    }

    void ClearVideoListeners()
    {
        WatchVideo.instance.Close -= OnVideoClose;
        WatchVideo.instance.Fail -= OnVideoFail;
        WatchVideo.instance.Reward -= OnVideoReward;
    }

    public void OnVideoClose()
    {
        ClearVideoListeners();
        ClearTextMessageListeners();
        Time.timeScale = 1;
    }

    public void OnVideoFail()
    {
        ClearVideoListeners();
        ClearTextMessageListeners();
        EventManagerMessages.instance.OK += OnMessageFailed;
        EventManagerMessages.instance.DispatchMessage("Sorry, no video available.\nYou can try again later.", true, false, false);
    }

    public void OnVideoReward()
    {
        m_defeatcontroller.ActivateSonicPacman(m_sonicPacmanI);
        ClearVideoListeners();
        ClearTextMessageListeners();
        EventManagerMessages.instance.OK += OnMessageEarned;
        EventManagerMessages.instance.DispatchMessage("You activated a Sonic Pacman!", true, false, false);
    }
    #endregion Video
}
