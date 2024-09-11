using System;
using UnityEngine;
using App.Main.Block;
using App.Main.Player;
using App.Main.Stage;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace App.Main.Stage
{
    public class StageSystem : MonoBehaviour
    {
        [SerializeField] private PlayerDatastore _player = default;
        [SerializeField] private GameObject _ballPrefab = default;
        [SerializeField] private GameObject _normalBlockPrefab = default;
        [SerializeField] private GameObject _targetBlockPrefab = default;

        [SerializeField] private int _finalStageNumberID = 5;

        private int _ballCountonStage = 0;
        public int BallCountonStage => _ballCountonStage;

        private int _normalBlockCount = 0;
        public int NormalBlockCount => _normalBlockCount;

        private int _targetBlockCount = 0;
        public int TargetBlockCount => _targetBlockCount;
        private int _clearedStageCount = 0;
        private int _roopCount = 0;
        private int _currentStageNumberID = 0;
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

        ///<summary>
        ///ボールを生成する
        ///</summary>
        ///<param name="position">生成する位置</param>
        public void CreateBall(Vector3 position)
        {
            GameObject ball = Instantiate(_ballPrefab, position, Quaternion.identity);
            ball.GetComponent<App.Main.Ball.Ball>().Initialize(_player, this);
            IncreaseBallCountonStage();
        }

        ///<summary>
        ///通常ブロックを生成する
        ///</summary>
        ///<param name="position">生成する位置</param>
        public void CreateNormalBlock(Vector3 position)
        {
            GameObject normalBlock = Instantiate(_normalBlockPrefab, position, Quaternion.identity);
            normalBlock.GetComponent<App.Main.Block.Block>().SetStage(this);
            IncreaseNormalBlockCount();
        }

        ///<summary>
        ///ターゲットブロックを生成する
        ///</summary>
        ///<param name="position">生成する位置</param>
        ///<param name="targetBlockData">生成するターゲットブロックのデータ</param>
        public void CreateTargetBlock(Vector3 position)
        {
            GameObject targetBlock = Instantiate(_targetBlockPrefab, position, Quaternion.identity);
            targetBlock.GetComponent<TargetBlock>().SetStage(this);
            IncreaseTargetBlockCount();
        }

        /// <summary>
        /// ステージの初期化
        /// </summary>
        /// <param name="_stagePatternID">現在のパターン</param>
        public void InitializeStage()
        {
            _ballCountonStage = 0;
            _normalBlockCount = 0;
            _targetBlockCount = 0;
            CreateBall(new Vector3(0, 0, 0));
            CreateNormalBlock(new Vector3(1, 1, 0));
            CreateTargetBlock(new Vector3(-1, 1, 0));
        }

        /// <summary>
        /// ステージのクリアカウントを増やす。また、それに伴い現在のステージ番号、ループ数を更新する。
        /// </summary>
        public void CountClearedStage()
        {
            _clearedStageCount++;
            _roopCount = _clearedStageCount/_finalStageNumberID;
            _currentStageNumberID = _clearedStageCount%_finalStageNumberID;
        }
    }
}
