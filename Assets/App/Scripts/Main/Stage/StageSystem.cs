using System;
using UnityEngine;
using App.Main.Block;
using App.Main.Player;
using App.Main.Stage;
using App.Main.Item;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace App.Main.Stage
{
    public class StageSystem : MonoBehaviour
    {
        [SerializeField] private ItemTable _itemTable = default;
        [SerializeField] private PlayerDatastore _player = default;
        [SerializeField] private GameObject _ballPrefab = default;
        [SerializeField] private GameObject _itemPrefab = default;
        [SerializeField] private int _finalStageNumberID = 5;
        private ItemSystem _itemSystem = default;
        private int _ballCountonStage = 0;
        public int BallCountonStage => _ballCountonStage;

        private int _normalBlockCount = 0;
        public int NormalBlockCount => _normalBlockCount;

        private int _targetBlockCount = 0;
        public int TargetBlockCount => _targetBlockCount;
        private int _clearedStageCount = 0;
        private int _roopCount = 1;
        private int _currentStageNumberID = 1;

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

        public void CreateItem(Vector3 position)
        {
            _itemSystem = new ItemSystem(_player);
            GameObject item = Instantiate(_itemPrefab, position, Quaternion.identity);
            item.GetComponent<App.Main.Item.Item>().Initialized(_itemTable, this, _player, _itemSystem.SelectItem());
        }

        public void CreateExpBall(Vector3 position)
        {
            _itemSystem = new ItemSystem(_player);
            GameObject item = Instantiate(_itemPrefab, position, Quaternion.identity);
            item.GetComponent<App.Main.Item.Item>().Initialized(_itemTable, this, _player, _itemSystem.SelectExp());
        }

        /// <summary>
        /// ステージの初期化
        /// </summary>
        public void InitializeStage()
        {
            _ballCountonStage = 0;
            _normalBlockCount = 0;
            _targetBlockCount = 0;
            CreateBall(new Vector3(0, 0, 0));
            GetComponent<BlockPattern>().CreateBlocks(_currentStageNumberID, _roopCount);
            _targetBlockCount = GetComponent<BlockPattern>().TargetBlockCount;
            _normalBlockCount = GetComponent<BlockPattern>().NormalBlockCount;
        }

        /// <summary>
        /// ステージのクリアカウントを増やす。また、それに伴い現在のステージ番号、ループ数を更新する。
        /// </summary>
        public void CountClearedStage()
        {
            _clearedStageCount++;
            _roopCount = _clearedStageCount / _finalStageNumberID;
            _currentStageNumberID = _clearedStageCount % _finalStageNumberID;
        }
    }
}
