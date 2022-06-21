using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource ballEffectAudioSource;
        
        public void PlayOneShotAudio(AudioClip audioClip)
        {
            ballEffectAudioSource.PlayOneShot(audioClip);
        }
    }
}
