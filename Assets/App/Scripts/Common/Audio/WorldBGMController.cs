using UnityEngine;
using App.Main.Stage;
using System.Collections;
using UnityEngine.Audio;

namespace App.Common.Audio
{
    /// <summary>
    /// ワールドごとのBGMを制御するクラス
    /// </summary>
    /// 現在のワールドIDに応じてBGMを再生する
    public class WorldBGMController : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _worldBGMList = default;
        [SerializeField] private ProcessSystem _processSystem = default;
        [SerializeField] private AudioMixer _audioMixer = default;
        bool Initialized = false;

        private void Start()
        {
            StartCoroutine(DelayedStart());
        }

        private IEnumerator DelayedStart()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();

            yield return new WaitForSeconds(0.01f);

            if (_worldBGMList.GetLength(0) == 0) yield break;

            _audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];
            _audioSource.clip = _worldBGMList[_processSystem.GetCurrentWorldNumberID() - 1];
            _audioSource.loop = true;
            _audioSource.volume = 0;
            _audioSource.Play();
            StartCoroutine(FadeIn());
            Debug.Log("BGMStarted");
            Initialized = true;
        }

        private void Update()
        {
            if (!Initialized) return;

            if (_worldBGMList.GetLength(0) == 0) return;

            if (_processSystem.IsPlaying() && !_audioSource.isPlaying)
            {
                _audioSource.Play();
                return;
            }
            else if (!_processSystem.IsPlaying() && _audioSource.isPlaying)
            {
                _audioSource.Stop();
                return;
            }
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
