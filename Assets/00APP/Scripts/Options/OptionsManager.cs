using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

    public delegate void DelegateResume();
    public event DelegateResume Resume;
    public delegate void DelegateOK();
    public event DelegateOK OK;
    public delegate void DelegateExit();
    public event DelegateExit Exit;
    public delegate void DelegateRestart();
    public event DelegateRestart Restart;
    public delegate void DelegateGDRP();
    public event DelegateGDRP GDRP;
    public delegate void DelegateCredits();
    public event DelegateCredits Credits;
    public delegate void DelegateChangeSFX(float volume);
    public event DelegateChangeSFX ChangeSFX;
    public delegate void DelegateChangeMusic(float volume);
    public event DelegateChangeMusic ChangeMusic;

    public GameObject m_btnResume;
    public GameObject m_btnOK;
    public GameObject m_btnExit;
    public GameObject m_btnRestart;
    public GameObject m_btnGDPR;
    public GameObject m_btnCredits;

    public Slider m_sliderSFX;
    public Slider m_sliderMusic;

    public bool m_displayResume;
    public bool m_displayExit;
    public bool m_displayRestart;
    public bool m_displayGDPR;
    public bool m_displayOK = true;
    public bool m_displayCredits;
    public bool m_hideAtStart;

    public bool m_hideOnButton = true;

    public static OptionsManager instance;

    private void Awake()
    {
        instance = this;
        m_btnResume.SetActive(m_displayResume);
        m_btnOK.SetActive(m_displayOK);
        m_btnExit.SetActive(m_displayExit);
        m_btnRestart.SetActive(m_displayRestart);
        m_btnGDPR.SetActive(m_displayGDPR);
        m_btnCredits.SetActive(m_displayCredits);
    }

    private void OnEnable()
    {
        DBsettings settings = DBmanager.Settings;
        m_sliderSFX.value = settings.sfxVolume;
        m_sliderMusic.value = settings.musicVolume;
    }

    private void OnDisable()
    {
        DBmanager.sfxVolume = m_sliderSFX.value;
        DBmanager.musicVolume = m_sliderMusic.value;
    }

    private void OnDestroy()
    {
        DBmanager.sfxVolume = m_sliderSFX.value;
        DBmanager.musicVolume = m_sliderMusic.value;
    }

    public void OnChangeSFX()
    {
        DispatchChangeSFX(m_sliderSFX.value);
    }

    public void OnChangeMusic()
    {
        DispatchChangeMusic(m_sliderMusic.value);
    }

    public void DispatchResume()
    {
        if (Resume != null)
        {
            Resume();
        }
    }

    public void DispatchOK()
    {
        if (OK != null)
        {
            OK();
        }
    }

    public void DispatchExit()
    {
        if (Exit != null)
        {
            Exit();
        }
    }

    public void DispatchRestart()
    {
        if (Restart != null)
        {
            Restart();
        }
    }

    public void DispatchGDRP()
    {
        if (GDRP != null)
        {
            GDRP();
        }
    }

    public void DispatchCredits()
    {
        if (Credits != null)
        {
            Credits();
        }
    }

    public void DispatchChangeSFX(float volume)
    {
        if (ChangeSFX != null)
        {
            ChangeSFX(volume);
        }
    }

    public void DispatchChangeMusic(float volume)
    {
        if (ChangeMusic != null)
        {
            ChangeMusic(volume);
        }
    }

    public void OnResume()
    {
        if(m_hideOnButton)
            gameObject.SetActive(false);
        DispatchResume();
    }

    public void OnOK()
    {
        if (m_hideOnButton)
            gameObject.SetActive(false);
        DispatchOK();
    }

    public void OnExit()
    {
        if (m_hideOnButton)
            gameObject.SetActive(false);
        DispatchExit();
    }

    public void OnRestart()
    {
        if (m_hideOnButton)
            gameObject.SetActive(false);
        DispatchRestart();
    }

    public void OnGDRP()
    {
        if (m_hideOnButton)
            gameObject.SetActive(false);
        DispatchGDRP();
    }

    public void OnCredits()
    {
        if (m_hideOnButton)
            gameObject.SetActive(false);
        DispatchCredits();
    }

    public void Display(bool enable)
    {
        gameObject.SetActive(enable);
    }
}
