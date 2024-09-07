using UnityEngine;
using App.Main.Player;
using System.Threading;

namespace App.Main.Stage
{
    public class ProcessSystem : MonoBehaviour
    {
        [SerializeField] private Timer _timer = default;
        [SerializeField] private PlayerDatastore _player = default;
        [SerializeField] private StageSystem _stageSystem = default;
        [SerializeField] private StageStateDatastore _stageState = default;
        
        void Start()
        {
            _stageState.SetPlayGame();
            _stageSystem.InitializeStage(1);
            _timer.InitializeTimer(60.0f);
            _timer.StartTimer();
        }

        void Update()
        {
            if (_stageState.isPlaying())
            {
                if (_stageSystem.BallCountonStage == 0)
                {
                    // 残機を減らす処理
                    _player.SubtractLive(1);
                    Debug.Log("Live: " + _player.Parameter.Live.CurrentValue);
                    if (_player.IsLiveValue(0)||(_timer.State == Timer.TimerState.TimeOver))
                    {
                        // ゲームオーバー処理
                        _stageState.SetGameOver();
                    }
                    else
                    {
                        // ボールを生成する
                        _stageSystem.CreateBall(new Vector3(0, 0, 0));
                    }
                }
                else if (_stageSystem.TargetBlockCount == 0)
                {
                    // クリア処理
                    _timer.StopTimer();
                    _stageState.SetClear();
                }
            }
        }
    }
}