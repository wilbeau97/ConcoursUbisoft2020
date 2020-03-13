using UnityEngine;

namespace Script.Audio
{
    [System.Serializable]
    public class Sound : MonoBehaviour
    {
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        [Range(-1f,1f)]
        public float SpacialSoundLeftRight;
        [Range(0f,1f)]
        public float SpacialSoundCloseFar;
        public bool loop;

        private AudioSource audioSource;

        public void SetAudioSource(AudioSource newAudioSource)
        {
            audioSource = newAudioSource;
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;
            audioSource.panStereo = SpacialSoundLeftRight;
            audioSource.spatialBlend = SpacialSoundCloseFar;
        }

        public void Play(string name)
        {
            if(audioClip.name.Equals(name))
            {
                audioSource.Play();
            }
        
        }

        public void Stop(string name)
        {
            if (audioClip.name.Equals(name))
            {
                audioSource.Stop();
            }
        }

        public float? getVolume(string name)
        {
            if (audioClip.name.Equals(name))
            {
                return audioSource.volume;
            }
            else
            {
                return null;
            }
        }

        internal void setVolume(string name, float volume)
        {
            if (audioClip.name.Equals(name))
            {
                audioSource.volume = volume;
            }
        }
    }
}

