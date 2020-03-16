using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Script.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] soundList;
        public static AudioManager Instance;
        public bool enableMainTheme = true;

        // Start is called before the first frame update
        void Start()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            foreach(Sound sound in soundList)
            {
                sound.SetAudioSource(gameObject.AddComponent<AudioSource>());
            }

            if (enableMainTheme)
            {
                Play("MainTheme");
            }
            Play("ambianceWindy");
        }
        public void Play(string nameSound)
        {
            foreach (Sound sound in soundList)
            {
                sound.Play(nameSound);
            }
        }

        public void Stop(string nameSound)
        {
            foreach (Sound sound in soundList)
            {
                sound.Stop(nameSound);
            }
        }

        public void SwitchScene(string namePreviousSound, string nameNextSound)
        {
            float volume = 0f;
            foreach (Sound sound in soundList)
            {
                volume = sound.getVolume(namePreviousSound) ?? 0f;
                if (volume != 0f)
                {
                    StartCoroutine(MusicLow(namePreviousSound, volume));
                    break;
                }
            }
            StartCoroutine(MusicHigh(nameNextSound, volume));
        }

        IEnumerator MusicLow(string namePreviousSound, float volume)
        {
            float vol = volume / 4;
            SetMusicVolume(namePreviousSound, volume - vol);
            yield return new WaitForSeconds(1f);
            SetMusicVolume(namePreviousSound, volume - 2 * vol);
            yield return new WaitForSeconds(1f);
            SetMusicVolume(namePreviousSound, volume - 3 * vol);
            yield return new WaitForSeconds(1f);
            Stop(namePreviousSound);

        }

        IEnumerator MusicHigh(string nameNextSound, float volume)
        {
            Play(nameNextSound);
            float vol = volume / 4;
            SetMusicVolume(nameNextSound, vol);
            yield return new WaitForSeconds(1f);
            SetMusicVolume(nameNextSound, 2 * vol);
            yield return new WaitForSeconds(1f);
            SetMusicVolume(nameNextSound, 3 * vol);
            yield return new WaitForSeconds(1f);
            SetMusicVolume(nameNextSound, volume);
        }

        public float GetMusicVolume(string name)
        {
            float? returned = null;
            foreach (Sound sound in soundList)
            {
                returned = sound.getVolume(name);
                if(returned != null)
                {
                    break;
                }
            }
            return returned ?? 0f;
        }

        public void SetMusicVolume(string name, float volume)
        {
            foreach (Sound sound in soundList)
            {
                sound.setVolume(name, volume);
            }
        }
    }
}

