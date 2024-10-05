using UnityEngine;
using App.Main.Player;
using App.Static;
using App.Common;

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
            _timer = GetComponent<Timer>();
            _stageSystem = GetComponent<StageSystem>();
            _stageState = GetComponent<StageStateDatastore>();
            _sceneLoader = GetComponent<SceneLoader>();
            InitializeGame();
            Debug.Log("_____________________________: " + _stageSystem.CurrentStageNumberID + " :_____________________________");
        }

        /// ゲームの更新処理
        void Update()
        {
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
            _stageSystem.SyncData();
            if (_stageSystem.ClearedStageCount == 0)
            {
                _timer.InitializeTimer(_timeLimit);
            }
            else
            {
                _timer.InitializeTimer(_timeLimit, StatisticsDatastore._remainingTimeLimit);
            }
            //_player.AddLive(StatisticsDatastore._remainingLive - _player.Parameter.Live.CurrentValue);
            //_player.SubtractLive(_player.Parameter.Live.CurrentValue - StatisticsDatastore._remainingLive);
        }

        /// static←インスタンス
        private void FetchGameParameter()
        {
            _stageSystem.FetchData();
            StatisticsDatastore.AssignRemainingTimeLimit(_timer.RemainingTimeLimit);
            StatisticsDatastore.AssignRemainingLive(_player.Parameter.Live.CurrentValue);
        }

        public void CurrentStageTime
    }
}
