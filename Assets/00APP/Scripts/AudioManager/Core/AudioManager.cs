using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AudioManagement
{

    [Serializable]
    public class ClipData
    {
        public AudioClip m_clip;
        public int m_maxAtOnce;
        public bool m_isConsumable;
        [HideInInspector]
        public int m_key;
        int m_clipsPlaying = 0;

        public void Init()
        {
            m_clipsPlaying = 0;
        }

        public ClipData GetClip()
        {
            if (m_clipsPlaying < m_maxAtOnce)
            {
                m_clipsPlaying++;
                return this;
            }
            return null;
        }

        public bool ReturnClip()
        {
            if (!m_isConsumable)
            {
                m_clipsPlaying--;
                return false;
            }
            else if(m_clipsPlaying == m_maxAtOnce) {
                return true;
            }
            return false;
        }

        public bool IsFull
        {
            get
            {
                return m_clipsPlaying >= m_maxAtOnce;
            }
        }
    }

    [Serializable]
    public class DictionaryClipData : SerializableDictionary<string, ClipData> { }

    public class AudioManager: MonoBehaviour
    {
        public AudioBank[] m_bank;
        public Dictionary<int, ClipData> m_clips = new Dictionary<int, ClipData>();
        public bool m_singleton = true;

        [HideInInspector]
        public WaitForSeconds END_CHECK = new WaitForSeconds(1);

        ClipPlayer[] m_playerFree;
        int m_playerFreeI;

        ClipData m_localClip;
        ClipPlayer m_localPlayer;

        public static AudioManager instance;

        void Awake()
        {
            if (m_singleton)
            {
                if (instance == null)
                {
                    instance = this;
                }
                else
                {
                    Destroy(gameObject);
                    return;
                }
            }

            int n;
            int hashCode;
            if (m_bank != null) {
                n = m_bank.Length;

                DictionaryClipData clips;
                ClipData clipData;
                for (int i = 0; i < n; i++)
                {
                    clips = m_bank[i].m_clips;
                    foreach (KeyValuePair<string, ClipData> kp in clips)
                    {
                        hashCode = kp.Key.GetHashCode();
                        kp.Value.Init();
                        if (m_clips.ContainsKey(hashCode))
                        {
                            clipData = m_clips[hashCode];
                            clipData.m_maxAtOnce += kp.Value.m_maxAtOnce;
                            m_clips[hashCode] = clipData;
                        }
                        else
                        {
                            m_clips[hashCode] = kp.Value;
                        }
                    }
                }
            }

            n = PlayersCount;
            m_playerFree = new ClipPlayer[PlayersCount];
            ClipPlayer player;
            m_playerFreeI = 0;
            foreach (KeyValuePair<int, ClipData> kp in m_clips)
            {
                n = kp.Value.m_maxAtOnce;
                kp.Value.m_key = kp.Key;
                for (int i = 0; i < n; i++)
                {
                    player = gameObject.AddComponent<ClipPlayer>();
                    player.m_manager = this;
                    m_playerFree[m_playerFreeI] = player;
                    m_playerFreeI++;
                }
            }
        }

        public int ClipsCount
        {
            get
            {
                return m_clips.Count;
            }
        }

        public int PlayersCount
        {
            get
            {
                int count = 0;
                foreach (KeyValuePair<int, ClipData> kp in m_clips)
                {
                    count += kp.Value.m_maxAtOnce;
                }
                return count;
            }
        }

        public ClipPlayer Play(int clipKey, bool loop = false)
        {
            if (m_playerFreeI < 1) {
                return null;
            }
            m_localClip = m_clips[clipKey];
            if (m_localClip.IsFull)
            {
                return null;
            }
            m_playerFreeI--;
            m_localPlayer = m_playerFree[m_playerFreeI];
            m_localPlayer.Play(m_localClip.GetClip(), loop);
            return m_localPlayer;
        }

        public void Stop(int clipKey)
        {
            for (int i = m_playerFreeI, n= m_playerFree.Length; i < n; i++)
            {
                m_localPlayer = m_playerFree[i];
                if (m_localPlayer.m_clip.m_key == clipKey)
                {
                    m_localPlayer.Stop();
                }
            }
        }

        public void Return(ClipPlayer player)
        {
            bool consume = player.m_clip.ReturnClip();
            if (consume) {
                Remove(player.m_clip.m_key);
            }
            m_playerFree[m_playerFreeI] = player;
            m_playerFreeI++;
        }

        public void Remove(int key)
        {
            m_clips.Remove(key);
        }

        public void SetVolume(float value)
        {
            for (int i = 0, n = m_playerFree.Length; i < n; i++)
            {
                m_playerFree[i].volume = value;
            }
        }
    }
}
