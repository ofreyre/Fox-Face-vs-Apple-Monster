using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManagement
{

    public class ClipPlayer : MonoBehaviour
    {
        AudioSource m_audioSource;
        public AudioManager m_manager;
        public ClipData m_clip;

        public void Awake()
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Play(ClipData clip, bool loop = false)
        {
            m_clip = clip;
            m_audioSource.loop = loop;
            m_audioSource.clip = clip.m_clip;
            if (loop)
            {
                m_audioSource.Play();
            }
            else
            {
                StartCoroutine(Track());
            }
        }

        public void Play()
        {
            m_audioSource.Play();
        }

        public void Pause()
        {
            if (m_audioSource != null)
            {
                m_audioSource.Pause();
            }
        }

        public void Stop()
        {
            m_audioSource.Stop();
            m_manager.Return(this);
        }

        IEnumerator Track()
        {
            m_audioSource.Play();
            yield return new WaitForSeconds(m_audioSource.clip.length);
            while (m_audioSource.isPlaying)
            {
                yield return m_manager.END_CHECK;
            }
            m_manager.Return(this);
        }

        public float volume
        {
            get { return m_audioSource.volume; }
            set { m_audioSource.volume = value; }
        }
    }
}
