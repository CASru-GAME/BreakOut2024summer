using UnityEngine;

namespace App.Main.Stage
{
    /// <summary>
    /// 制限時間を管理するクラス、単位は秒
    /// </summary>
    public class Timer : MonoBehaviour
    {
        public enum TimerState
        {
            Waiting,
            Playing,
            TimeOver
        }
        private TimerState _state = TimerState.Waiting;
        /// <summary>
        /// 制限時間の状態、こっちを基本参照する
        /// </summary>
        public TimerState State => _state;
        private float _timeLimit;
        private float _remainingTimeLimit;
        /// <summary>
        /// 残りの制限時間、実際に減る値、単位は秒
        /// </summary>
        public float RemainingTimeLimit => _remainingTimeLimit;
        /// <summary>
        /// 制限時間の初期化
        /// </summary>
        public void InitializeTimer(float timeLimit)
        {
            _timeLimit = timeLimit;
        }
        /// <summary>
        /// 制限時間のリセット
        /// </summary>
        public void ResetTimer()
        {
            _remainingTimeLimit = _timeLimit;
        }
        /// <summary>
        /// タイマーを開始する
        /// </summary>
        public void StartTimer()
        {
            _state = TimerState.Playing;
        }
        /// <summary>
        /// タイマーを停止する
        /// </summary>
        public void StopTimer()
        {
            _state = TimerState.Waiting;
        }
        void Update()
        {
            if ((_remainingTimeLimit > 0)&&(_state == TimerState.Playing))
            {
                _remainingTimeLimit -= Time.deltaTime;
                if (_timeLimit <= 0)
                {
                    _state = TimerState.TimeOver;
                    Debug.Log("TimerState: " + _state);
                    _remainingTimeLimit = 0;
                }
            }
        }
    }
}