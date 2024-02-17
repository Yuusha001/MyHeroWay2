using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class AudioManager : Singleton<AudioManager>
    {
        public static int MusicSetting
        {
            get { return PlayerPrefs.GetInt("bt_music_setting", 1); }
            set { PlayerPrefs.SetInt("bt_music_setting", value); }
        }
        public static int SoundSetting
        {
            get { return PlayerPrefs.GetInt("bt_sound_setting", 1); }
            set { PlayerPrefs.SetInt("bt_sound_setting", value); }
        }
        public static int VibrateSetting
        {
            get { return PlayerPrefs.GetInt("key_config_vibrate", 1); }
            set { PlayerPrefs.SetInt("key_config_vibrate", value); }
        }
        public AudioSource music;
        public AudioSource sfx;
        public AudioSource ambience;
        public AudioClip[] songs;
        public AudioClip[] sounds;
        public AudioClip[] ambiences;
        public async void PlayOneShot(AudioClip audioClip, float volume, float delay = 0, Transform target = null)
        {
            if (SoundSetting != 1) return;
            if (audioClip == null) return;
            await Utils.Delay.DoAction(() =>
            {
                float newVolume = volume;
                sfx.PlayOneShot(audioClip, newVolume);
            }, delay);
        }

        public void PlayMusic(float volume, bool isLoop, AudioClip audio = null)
        {
            if (MusicSetting != 1)
            {
                volume = 0;
            }
            AudioClip clip = audio == null ? songs[Random.Range(0, songs.Length - 1)] : audio;
            if (clip == null) return;
            AudioSource source = music;
            source.clip = clip;
            source.loop = isLoop;
            source.volume = volume;
            source.Play();
        }

        public void PlayAmbience(string clipName, float volume, bool isLoop)
        {
            if (MusicSetting != 1) return;
            AudioClip clip = System.Array.Find(ambiences, s => s.name == clipName);
            if (clip == null) return;
            ambience.clip = clip;
            ambience.loop = isLoop;
            ambience.volume = volume;
            ambience.Play();
        }

        public void MusicHandler()
        {
            MusicSetting = MusicSetting == 1 ? 0 : 1;
            music.volume = MusicSetting;
        }

        public void SFXHandler()
        {
            SoundSetting = SoundSetting == 1 ? 0 : 1;
            sfx.volume = SoundSetting;
        }

        public void VibrateHandler()
        {
            VibrateSetting = VibrateSetting == 1 ? 0 : 1;
            
        }
    }
}
