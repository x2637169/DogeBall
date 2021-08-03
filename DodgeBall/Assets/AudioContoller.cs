using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContoller : MonoBehaviour
{
    [SerializeField] private AudioSource m_audio;
    [SerializeField] public ContollerAudioClip m_audioClip;

    public void PlayOneShotSound(AudioClip _audioClip)
    {
        m_audio.PlayOneShot(_audioClip);
    }

    [System.Serializable]
    public class ContollerAudioClip
    {
        public AudioClip StartSound;
        public AudioClip ReStartSound;
        public AudioClip GameOverSound;
    }
}
