using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using App.Common.Data.PlayerPrefs;
using UnityEngine.SceneManagement;

namespace App.Title.Setting
{
    public class VolumeSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider slider_Master;
        [SerializeField] private Slider slider_BGM;
        [SerializeField] private Slider slider_SE;
        static private bool Initialized = false;

        private void Start()
        {
            if (!Initialized)
            {
                PlayerPrefsSystem.LoadVolumeSetting(out float master_Volume, out float bgm_Volume, out float se_Volume);
                SetVolume_All(master_Volume, bgm_Volume, se_Volume);
                Initialized = true;
            }
            audioMixer.GetFloat("Master", out float masterVolume);
            slider_Master.value = masterVolume;
            audioMixer.GetFloat("BGM", out float bgmVolume);
            slider_BGM.value = bgmVolume;
            audioMixer.GetFloat("SE", out float seVolume);
            slider_SE.value = seVolume;
        }

        void OnDestroy()
        {
            PlayerPrefsSystem.SaveVolumeSetting(slider_Master.value, slider_BGM.value, slider_SE.value);
        }

        public void SetVolume_Master(float volume)
        {
            audioMixer.SetFloat("Master", volume);
        }

        public void SetVolume_BGM(float volume)
        {
            audioMixer.SetFloat("BGM", volume);
        }

        public void SetVolume_SE(float volume)
        {
            audioMixer.SetFloat("SE", volume);
        }

        public void SetVolume_All(float volume)
        {
            audioMixer.SetFloat("Master", volume);
            audioMixer.SetFloat("BGM", volume);
            audioMixer.SetFloat("SE", volume);
        }

        public void SetVolume_All(float volume_Master, float volume_BGM, float volume_SE)
        {
            audioMixer.SetFloat("Master", volume_Master);
            audioMixer.SetFloat("BGM", volume_BGM);
            audioMixer.SetFloat("SE", volume_SE);
        }
    }
}