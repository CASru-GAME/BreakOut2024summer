using System;
using UnityEngine;

namespace App.Main.Stage
{
    public class StageSystem : MonoBehaviour
    {
        private enum StageState
        {
            Waiting,
            Playing,
            GameOver,
            Clear
        }
        private StageState _state = StageState.Waiting;


        private int _ballCountonStage = 0;
        public int BallCountonStage => _ballCountonStage;

        private int _normalBlockCount = 0;
        public int NormalBlockCount => _normalBlockCount;

        private int _targetBlockCount = 0;
        public int TargetBlockCount => _targetBlockCount;

        ///<summary>
        ///ステージシステム上のボールの数を一つ増やす。
        ///</summary>
        public void IncreaseBallCountonStage()
        {
            ++_ballCountonStage;
            Debug.Log("BallCountonStage: " + _ballCountonStage);
        }
        ///<summary>
        ///ステージシステム上のボールの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">ボールの数が0未満になる場合に発生します。</exception>
        public void DecreaseBallCountonStage()
        {
            if (_ballCountonStage <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_ballCountonStage;
            Debug.Log("BallCountonStage: " + _ballCountonStage);
        }

        ///<summary>
        ///ステージシステム上の通常ブロックの数を一つ増やす。
        ///</summary>
        public void IncreaseNormalBlockCount()
        {
            ++_normalBlockCount;
            Debug.Log("NormalBlockCount: " + _normalBlockCount);
        }
        ///<summary>
        ///ステージシステム上の通常ブロックの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">通常ブロックの数が0未満になる場合に発生します。</exception>
        public void DecreaseNormalBlockCount()
        {
            if (_normalBlockCount <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_normalBlockCount;
            Debug.Log("NormalBlockCount: " + _normalBlockCount);
        }

        ///<summary>
        ///ステージシステム上のターゲットブロックの数を一つ増やす。
        ///</summary>
        public void IncreaseTargetBlockCount()
        {
            ++_targetBlockCount;
            Debug.Log("TargetBlockCount: " + _targetBlockCount);
        }
        ///<summary>
        ///ステージシステム上のターゲットブロックの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">ターゲットブロックの数が0未満になる場合に発生します。</exception>
        public void DecreaseTargetBlockCount()
        {
            if (_targetBlockCount <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_targetBlockCount;
            Debug.Log("TargetBlockCount: " + _targetBlockCount);
        }

        void Start()
        {
            // ステージの初期化処理
            _state = StageState.Playing;
        }

        void Update()
        {
            if (_state == StageState.Playing)
            {
                if (_ballCountonStage == 0)
                {
                    // ゲームオーバー処理
                }
                else if (_targetBlockCount == 0)
                {
                    // クリア処理
                }
            }
        }
    }
}