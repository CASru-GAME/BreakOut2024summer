using System.Collections;
using UnityEngine;

namespace App.Common.Audio
{
    public class IndividualAudioFadeIn : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            StartCoroutine(FadeIn());
        }
        
        IEnumerator FadeIn()
        {
            for (float i = 0; i <= 1; i += 0.01f)
            {
                _audioSource.volume = i;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}