using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class GameAudioPlayer : MonoBehaviour {
    public static GameAudioPlayer instance;

    public AudioManager m_managerSFX;
    public AudioManager m_managerMusic;
    public OptionsManager m_options;

    int audio_arrive = "arrive".GetHashCode();
    int[] audio_bulletHit = { "bulletHit0".GetHashCode(), "bulletHit1".GetHashCode() , "bulletHit2".GetHashCode(), "bulletHit3".GetHashCode() };
    int audio_catapult = "catapult".GetHashCode();
    int[] audio_diePacman = { "diePacman0".GetHashCode() , "diePacman1".GetHashCode(), "laugh0".GetHashCode(), "laugh1".GetHashCode(), "laugh2".GetHashCode(), "laugh3".GetHashCode(), "laugh4".GetHashCode(), "laugh5".GetHashCode(), "laugh6".GetHashCode(), "laugh7".GetHashCode() };
    int[] audio_dieMinion = { "dieMinion0".GetHashCode(), "dieMinion1".GetHashCode(), "dieMinion2".GetHashCode(), "dieMinion3".GetHashCode(), "dieMinion4".GetHashCode() };
    int[] audio_minionAttack = { "minionAttack0".GetHashCode(), "minionAttack1".GetHashCode(), "minionAttack2".GetHashCode(), "minionAttack3".GetHashCode(), "minionAttack4".GetHashCode() };
    int[] audio_pacmanAttack = { "pacmanAttack0".GetHashCode(), "pacmanAttack1".GetHashCode(), "pacmanAttack2".GetHashCode(), "pacmanAttack3".GetHashCode() };
    int audio_musicBegin = "musicBegin".GetHashCode();
    int audio_musicMarch = "musicMarch".GetHashCode();
    int audio_musicLose = "musicLose".GetHashCode();
    int audio_musicWin = "musicWin".GetHashCode();
    int audio_explosion_short = "explosion_short0".GetHashCode();
    int[] audio_explosion_mid = { "explosion_mid0".GetHashCode(), "explosion_mid1".GetHashCode(), "explosion_mid2".GetHashCode(), "explosion_mid3".GetHashCode() };
    int[] audio_explosion_long = { "explosion_long0".GetHashCode(), "explosion_long1".GetHashCode(), "explosion_long2".GetHashCode() };
    int audio_gameCoin = "gameCoin".GetHashCode();
    int audio_collectCoin = "collectCoin".GetHashCode();
    int audio_sonicpacman = "sonicpacman".GetHashCode();
    int audio_collectBabypacman = "collectBabypacman".GetHashCode();
    int audio_spawnBabypacman = "spawnBabypacman".GetHashCode();
    
    ClipPlayer m_localPlayer;
    int counter;
    int audio_bulletHit_n;
    int audio_diePacman_n;
    int audio_dieMinion_n;
    int audio_minionAttack_n;
    int audio_pacmanAttackn_n;
    int audio_explosion_mid_n;
    int audio_explosion_long_n;
    
    ClipPlayer m_clipPlayer_playing;


    public void Init()
    {
        instance = this;

        m_options.ChangeSFX += OnChangeSFX;
        m_options.ChangeMusic += OnChangeMusic;

        audio_bulletHit_n = audio_bulletHit.Length;
        audio_diePacman_n = audio_diePacman.Length;
        audio_dieMinion_n = audio_dieMinion.Length;
        audio_minionAttack_n = audio_minionAttack.Length;
        audio_pacmanAttackn_n = audio_pacmanAttack.Length;
        audio_explosion_mid_n = audio_explosion_mid.Length;
        audio_explosion_long_n = audio_explosion_long.Length;

        VolumesFomDB();
    }

    void OnChangeSFX(float volume)
    {
        m_managerSFX.SetVolume(volume);
    }

    void OnChangeMusic(float volume)
    {
        m_managerMusic.SetVolume(volume);
    }

    #region State sounds

    public void PlayStart()
    {
        m_managerSFX.Play(audio_musicBegin);
    }

    public void PlayPlaying()
    {
        m_localPlayer = m_managerMusic.Play(audio_musicMarch, true);
        if (m_localPlayer != null)
        {
            m_clipPlayer_playing = m_localPlayer;
        }
    }

    public void StopPlaying()
    {
        if (m_clipPlayer_playing != null)
        {
            m_clipPlayer_playing.Stop();
        }
    }

    public void PlayArrive()
    {
        m_managerSFX.Play(audio_arrive);
    }

    public ClipPlayer PlaySonicpacman()
    {
        return m_managerSFX.Play(audio_sonicpacman, true);
    }

    public void PlayWin()
    {
        m_managerSFX.Play(audio_musicWin);
    }

    public void PlayLose()
    {
        m_managerSFX.Play(audio_musicLose);
    }
    #endregion State sounds

    public void PlayShoot()
    {
        counter++;
        m_managerSFX.Play(audio_pacmanAttack[counter % audio_pacmanAttackn_n]);
    }

    public void PlayShootEliptic()
    {
        m_managerSFX.Play(audio_catapult);
    }

    public void PlayBulletHit()
    {
        counter++;
        m_managerSFX.Play(audio_bulletHit[counter % audio_bulletHit_n]);
    }

    public void PlayMinionDie()
    {
        counter++;
        m_managerSFX.Play(audio_dieMinion[counter % audio_dieMinion_n]);
    }

    public void PlayMinionAttack()
    {
        counter++;
        m_managerSFX.Play(audio_minionAttack[counter % audio_minionAttack_n]);
    }

    public void PlayPacmanDie()
    {
        counter++;
        m_managerSFX.Play(audio_diePacman[counter % audio_diePacman_n]);
    }

    public void PlayExplosionShort()
    {
        m_managerSFX.Play(audio_explosion_short);
    }

    public void PlayExplosionMid()
    {
        counter++;
        m_managerSFX.Play(audio_explosion_mid[counter % audio_explosion_mid_n]);
    }

    public void PlayExplosionLong()
    {
        counter++;
        m_managerSFX.Play(audio_explosion_long[counter % audio_explosion_long_n]);
    }
    
    public void PlayCoin()
    {
        m_managerSFX.Play(audio_gameCoin);
    }

    public void PlayCollectCoin()
    {
        m_managerSFX.Play(audio_collectCoin);
    }

    public void PlayCollectBabypacman()
    {
        m_managerSFX.Play(audio_collectBabypacman);
    }

    public void PlaySpawnBabypacman()
    {
        m_managerSFX.Play(audio_spawnBabypacman);
    }

    public void VolumesFomDB()
    {
        m_managerSFX.SetVolume(DBmanager.sfxVolume);
        m_managerMusic.SetVolume(DBmanager.musicVolume);
    }
}
