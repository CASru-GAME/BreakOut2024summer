using UnityEngine;

namespace App.Main.Stage
{
    public class StageStateDatastore : MonoBehaviour
    {
        [SerializeField] StageSystem _stageSystem;
        public enum StageState
        {
            Waiting,
            Playing,
            GameOver,
            StageClear,
            AllStageClear
        }
        private StageState _state = StageState.Waiting;
        /// <summary>
        /// ゲームの状態をPlayingにセットする
        /// </summary>
        public void SetPlayGame()
        {
            _state = StageState.Playing;
            Debug.Log("State: " + _state);
        }
        /// <summary>
        /// ゲームの状態をGameOverにセットする
        /// </summary>
        public void SetGameOver()
        {
            _state = StageState.GameOver;
            Debug.Log("State: " + _state);
        }
        /// <summary>
        /// ゲームの状態をStageClearにセットする
        /// </summary>
        public void SetClear()
        {
            _state = StageState.StageClear;
            Debug.Log("State: " + _state);
        }
        /// <summary>
        /// ゲームの状態をWaitingにセットする
        /// </summary>
        public void SetWaiting()
        {
            _state = StageState.Waiting;
            Debug.Log("State: " + _state);
        }
        /// <summary>
        /// ゲームの状態をAllStageClearにセットする
        /// </summary>
        public void SetAllStageClear()
        {
            _state = StageState.AllStageClear;
            Debug.Log("State: " + _state);
        }
        /// <summary>
        /// ゲームがPlayingかどうかを返す
        /// </summary>
        public bool isPlaying()
        {
            return _state == StageState.Playing;
        }
        /// <summary>
        /// ゲームがGameOverかどうかを返す
        /// </summary>
        public bool isGameOver()
        {
            return _state == StageState.GameOver;
        }
        /// <summary>
        /// ゲームがStageClearかどうかを返す
        /// </summary>
        public bool isClear()
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
        /// <summary>
        /// ゲームがAllStageClearかどうかを返す
        /// </summary>
        public bool isAllStageClear()
        {
            return _state == StageState.AllStageClear;
        }
    }
}