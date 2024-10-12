using UnityEngine;
using App.Main.Player;
using App.Static;
using App.Common;
using System;
using System.Collections;

namespace App.Main.Stage
{
    public class ProcessSystem : MonoBehaviour
    {
        private Timer _timer = default;
        private StageSystem _stageSystem = default;
        private StageStateDatastore _stageState = default;
        private SceneLoader _sceneLoader = default;
        [SerializeField] private PlayerDatastore _player = default;
        [SerializeField] private float _timeLimit = 60.0f;


        /// ゲームの初期化処理
        void Start()
        {
            StartCoroutine(DelayedStart());
        }

        /// <summary>
        /// 遅延実行される初期化処理
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelayedStart()
        {
            yield return new WaitForSeconds(0.001f); // 1秒の遅延

            _timer = GetComponent<Timer>();
            _stageSystem = GetComponent<StageSystem>();
            _stageState = GetComponent<StageStateDatastore>();
            _sceneLoader = GetComponent<SceneLoader>();
            InitializeGame();
            Debug.Log("_____________________________: CurrentStageNumberID = " + _stageSystem.CurrentStageNumberID + " :_____________________________");
            Debug.Log("_____________________________: Static → ClearedStageCount = " + _stageSystem.ClearedStageCount + " :_____________________________");
            Debug.Log("_____________________________: Static → TotalDestroyedTargetBlockCount = " + _stageSystem.GetTotalCat() + " :_____________________________");
            Debug.Log("_____________________________: Static → RemainingTimeLimit = " + _timer.RemainingTimeLimit + " :_____________________________");
            Debug.Log("_____________________________: Static → RemainingLive = " + _player.Parameter.GetLiveValue() + " :_____________________________");
            Debug.Log("_____________________________: Static → TotalAquiredExperiencePoint = " + _player.GetExperiencePointValue() + " :_____________________________");
            Debug.Log("_____________________________: Static → PlayerLevel = " + _player.GetLevelValue() + " :_____________________________");
            Debug.Log("_____________________________: Static → TotalAquiredPerkList = " + _player.PerkSystem.PerkList.GetOwnedPerkList() + " :_____________________________");
        }

        /// ゲームの更新処理
        void Update()
        {
            if(_stageState == null) return;
            if (_stageState.isPlaying())
            {
                //　時間切れになったら
                if (_timer.State == Timer.TimerState.TimeOver)
                {
                    // ゲーム終了処理
                    _stageState.SetGameFinish();
                }
                //画面上のボールがなくなったら
                else if (_stageSystem.BallCountonStage == 0)
                {
                    // 残機を減らす処理
                    _player.SubtractLive(1);
                    Debug.Log("Live: " + _player.Parameter.Live.CurrentValue);
                    // 残機が0になったら
                    if (_player.IsLiveValue(0))
                    {
                        // ゲーム終了処理
                        _stageState.SetGameFinish();
                    }
                    else
                    {
                        // ボールを生成する
                        _stageSystem.CreateBall(new Vector3(0, 0, 0));
                    }
                }
                // ターゲットブロックがすべてなくなったら
                else if (_stageSystem.TargetBlockCount == 0)
                {
                    // ステージクリア処理
                    _stageState.SetStageClear();
                    PerkEffect();
                }
            }
            else if (_stageState.isGameFinish())
            {
                // ゲーム終了処理
                ConductGameFinishProcess();
            }
            else if (_stageState.isStageClear())
            {
                // ステージクリア処理
                ConductStageClearProcess();
            }
        }

        private void PerkEffect()
        {
            if(_player.PerkSystem.PerkList.AllPerkList[11].IntEffect() == 0)
            {
                return;
            }
            if(_player.PerkSystem.PerkList.AllPerkList[11].IntEffect() == 1)
            {
                _player.AddLive(1);
            }

        }

        // ゲームの状態がゲーム終了状態になった際のゲーム終了処理
        private void ConductGameFinishProcess()
        {
            _stageState.SetWaiting();
            _timer.StopTimer();
            FetchGameParameter();
            _sceneLoader.LoadSceneAsyncByName("ResultScene");
        }

        // ゲームの状態がステージクリア状態になった際のステージクリア処理
        private void ConductStageClearProcess()
        {
            _stageState.SetWaiting();
            _stageSystem.CountClearedStage();
            _timer.StopTimer();
            FetchGameParameter();
            _sceneLoader.LoadSceneAsyncByName("MainScene");
        }

        // どのような状態であっても発動するゲームの初期化処理
        private void InitializeGame()
        {
            LoadGameParameter();
            _stageSystem.InitializeStage();
            _stageState.SetPlayGame();
            _timer.StartTimer();
        }

        /// インスタンス←static
        private void LoadGameParameter()
        {
            // クリアしたステージ数と助けた猫の総数のロード
            _stageSystem.SyncData();
            // 残りタイムリミットのロード
            if (_stageSystem.ClearedStageCount == 0)
            {
                _timer.InitializeTimer(_timeLimit);
            }
            else
            {
                _timer.InitializeTimer(_timeLimit, StatisticsDatastore._remainingTimeLimit);
                // 残り残機のロード
                if (StatisticsDatastore._remainingLive > _player.Parameter.GetLiveValue())
                {
                    _player.AddLive(StatisticsDatastore._remainingLive - _player.Parameter.GetLiveValue());
                }
                else if (StatisticsDatastore._remainingLive < _player.Parameter.GetLiveValue())
                {
                    _player.SubtractLive(_player.Parameter.GetLiveValue() - StatisticsDatastore._remainingLive);
                }
            }
            _player.LoadLevelAndExperiencePoint(StatisticsDatastore._playerLevel, StatisticsDatastore._totalAquiredExperiencePoint);
            _player.PerkSystem.PerkList.LoadPerkList(StatisticsDatastore._totalAquiredPerkList);
        }

        /// static←インスタンス
        private void FetchGameParameter()
        {
            _stageSystem.FetchData();
            StatisticsDatastore.AssignRemainingTimeLimit(_timer.RemainingTimeLimit);
            StatisticsDatastore.AssignRemainingLive(_player.Parameter.GetLiveValue());
            StatisticsDatastore.AssignTotalAquiredExperiencePoint(_player.GetExperiencePointValue());
            StatisticsDatastore.AssignPlayerLevel(_player.GetLevelValue());
            StatisticsDatastore.AssignTotalAquiredPerkList(_player.PerkSystem.PerkList.GetOwnedPerkList());
        }

        /// <summary>
        /// 現在のステージが開始されてからの経過時間を取得する
        /// </summary>
        /// <returns>現在のステージが開始されてからの経過時間</returns>
        public float GetTime_afterCurrentStageStarted()
        {
            if (_stageSystem.CurrentStageNumberID == 1) return _timeLimit - _timer.RemainingTimeLimit;
            return StatisticsDatastore._remainingTimeLimit - _timer.RemainingTimeLimit;
        }

        public float GetRemainingTimerLimit()
        {
            if(_timer == null) return -1f;
            return _timer.RemainingTimeLimit;
        }
    }
}
