using UnityEngine;

namespace App.Main.Stage
{
    public class StageStateDatastore : MonoBehaviour
    {
        public enum StageState
        {
            Waiting,
            Playing,
            GameFinish,
            StageClear
        }
        private StageState _state = StageState.Waiting;
        /// <summary>
        /// ゲームの状態をPlayingにセットする
        /// </summary>
        public void SetPlayGame()
        {
            _state = StageState.Playing;
        }
        /// <summary>
        /// ゲームの状態をGameFinishにセットする
        /// </summary>
        public void SetGameFinish()
        {
            _state = StageState.GameFinish;
        }
        /// <summary>
        /// ゲームの状態をStageClearにセットする
        /// </summary>
        public void SetStageClear()
        {
            _state = StageState.StageClear;
        }
        /// <summary>
        /// ゲームの状態をWaitingにセットする
        /// </summary>
        public void SetWaiting()
        {
            _state = StageState.Waiting;
        }
        /// <summary>
        /// ゲームがPlayingかどうかを返す
        /// </summary>
        public bool isPlaying()
        {
            return _state == StageState.Playing;
        }
        /// <summary>
        /// ゲームがGameFinishかどうかを返す
        /// </summary>
        public bool isGameFinish()
        {
            return _state == StageState.GameFinish;
        }
        /// <summary>
        /// ゲームがStageClearかどうかを返す
        /// </summary>
        public bool isStageClear()
        {
            return _state == StageState.StageClear;
        }
        /// <summary>
        /// ゲームがWaitingかどうかを返す
        /// </summary>
        public bool isWaiting()
        {
            return _state == StageState.Waiting;
        }
    }
}