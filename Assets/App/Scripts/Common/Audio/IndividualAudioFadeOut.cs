using UnityEngine;
using System.Collections;

namespace App.Common.Audio
{
    public class IndividualAudioFadeOut : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            StartCoroutine(FadeOut());
        }
        
        IEnumerator FadeOut()
        {
            for (float i = 1; i >= 0; i -= 0.01f)
            {
                _audioSource.volume = i;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}