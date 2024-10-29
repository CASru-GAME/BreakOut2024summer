using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace App.Title.Setting
{
    public class VolumeSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider slider_Master;
        [SerializeField] private Slider slider_BGM;
        [SerializeField] private Slider slider_SE;

        private void Start()
        {
            audioMixer.GetFloat("Master", out float masterVolume);
            slider_Master.value = masterVolume;
            audioMixer.GetFloat("BGM", out float bgmVolume);
            slider_BGM.value = bgmVolume;
            audioMixer.GetFloat("SE", out float seVolume);
            slider_SE.value = seVolume;
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
    }
}