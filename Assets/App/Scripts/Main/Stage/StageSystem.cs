using UnityEngine;

namespace App.Main.Stage
{
    public class StageSystem : MonoBehaviour
    {
        private int _ballCountonStage = 0;
        public int BallCountonStage => _ballCountonStage;

        private int _normalBlockCount = 0;
        public int NormalBlockCount => _normalBlockCount;

        private int _targetBlockCount = 0;
        public int TargetBlockCount => _targetBlockCount;

        ///<summary>
        ///ステージシステム上のボールの数を一つ増やす。
        ///</summary>
        public IncreaseBallCountonStage()
        {
            ++_ballCountonStage;
        }
        ///<summary>
        ///ステージシステム上のボールの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">ボールの数が0未満になる場合に発生します。</exception>
        public DecreaseBallCountonStage()
        {
            if (_ballCountonStage <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_ballCountonStage;
        }

        ///<summary>
        ///ステージシステム上の通常ブロックの数を一つ増やす。
        ///</summary>
        public IncreaseNormalBlockCount()
        {
            ++_normalBlockCount;
        }
        ///<summary>
        ///ステージシステム上の通常ブロックの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">通常ブロックの数が0未満になる場合に発生します。</exception>
        public DecreaseNormalBlockCount()
        {
            if (_normalBlockCount <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_normalBlockCount;
        }

        ///<summary>
        ///ステージシステム上のターゲットブロックの数を一つ増やす。
        ///</summary>
        public IncreaseTargetBlockCount()
        {
            ++_targetBlockCount;
        }
        ///<summary>
        ///ステージシステム上のターゲットブロックの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">ターゲットブロックの数が0未満になる場合に発生します。</exception>
        public DecreaseTargetBlockCount()
        {
            if (_targetBlockCount <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_targetBlockCount;
        }

        void Start()
        {
        }

        void Update()
        {
        }
    }
}