using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Audio;

namespace App.Common.Audio
{
    public class WholeSECollector : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _wholeSEList = default;
        [SerializeField] private AudioMixer _audioMixer = default;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SE")[0];
        }

        /// <summary>
        /// 指定されたSEを再生する
        /// </summary>
        /// <param name="seID">再生するSEのID</param>
        public void PlaySE(int seID)
        {
            if (_wholeSEList.GetLength(0) == 0) return;

            _audioSource.PlayOneShot(_wholeSEList[seID]);
        }

        /// <summary>
        /// 決定SEを再生する
        /// </summary>
        public void PlayDecisionSE()
        {
            PlaySE(0);
        }

        /// <summary>
        /// パーク獲得時のSEを再生する
        /// </summary>
        public void PlayParkGetSE()
        {
            PlaySE(4);
        }
    }
}