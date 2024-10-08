using System;
using UnityEngine;
using App.Main.Player;
using App.Main.Item;
using App.Static;

namespace App.Main.Stage
{
    public class StageSystem : MonoBehaviour
    {
        [SerializeField] private ItemTable _itemTable = default;
        [SerializeField] private PlayerDatastore _player = default;
        [SerializeField] private GameObject _ballPrefab = default;
        [SerializeField] private GameObject _itemPrefab = default;
        [SerializeField] private int _finalStageNumberID = 15;
        [SerializeField] public GameObject Canvas = default;
        private ItemSystem _itemSystem = default;
        private int _ballCountonStage = 0;
        public int BallCountonStage => _ballCountonStage;

        private int _normalBlockCount = 0;
        public int NormalBlockCount => _normalBlockCount;

        private int _targetBlockCount = 0;
        public int TargetBlockCount => _targetBlockCount;
        private int _totalDestroyedTargetBlockCount = 0;
        private int _clearedStageCount = 0;
        public int ClearedStageCount => _clearedStageCount;
        private int _roopCount = 1;
        public int RoopCount => _roopCount;
        private int _currentStageNumberID = 1;
        public int CurrentStageNumberID => _currentStageNumberID;
        private int _currentWorldNumberID = 1;

        ///<summary>
        ///ステージシステム上のボールの数を一つ増やす。
        ///</summary>
        public void IncreaseBallCountonStage()
        {
            ++_ballCountonStage;
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
        }

        ///<summary>
        ///ステージシステム上の通常ブロックの数を一つ増やす。
        ///</summary>
        public void IncreaseNormalBlockCount()
        {
            ++_normalBlockCount;
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
        }

        ///<summary>
        ///ステージシステム上のターゲットブロックの数を一つ増やす。
        ///</summary>
        public void IncreaseTargetBlockCount()
        {
            ++_targetBlockCount;
        }

        ///<summary>
        ///ステージシステム上のターゲットブロックの数を一つ減らす。
        ///</summary>
        ///<exception cref="ArgumentException">ターゲットブロックの数が0未満になる場合に発生します。</exception>
        ///<remark>
        ///同時に、破壊されたターゲットブロックの数を一つ増やす。
        ///</remark>
        public void DecreaseTargetBlockCount()
        {
            if (_targetBlockCount <= 0)
            {
                throw new ArgumentException("Value cannot be negative");
            }
            --_targetBlockCount;
            ++_totalDestroyedTargetBlockCount;
        }

        ///<summary>
        ///ステージシステム上の破壊されたターゲットブロックの数を取得する。
        /// <returns>破壊されたターゲットブロックの数</returns>
        /// </summary>
        public int GetTotalDestroyedTargetBlockCount()
        {
            return _totalDestroyedTargetBlockCount;
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
        /// ステージのクリアカウントを増やす。また、それに伴い現在のステージ番号、ループ数、ワールドIDを更新する。
        /// </summary>
        public void CountClearedStage()
        {
            _clearedStageCount++;
            _roopCount = _clearedStageCount / _finalStageNumberID + 1;
            _currentStageNumberID = _clearedStageCount % _finalStageNumberID + 1;
            _currentWorldNumberID = _currentStageNumberID / 3 + (2 / 3);
        }

        /// <summary>
        /// 前のステージのクリアカウントを取得する
        /// </summary>
        public void SyncData()
        {
            for (int i = 0; i < StatisticsDatastore._totalClearedStage; i++)
            {
                CountClearedStage();
            }
            for (int i = 0; i < StatisticsDatastore._totalDestroyedTargetBlock; i++)
            {
                _totalDestroyedTargetBlockCount++;
            }
        }

        /// <summary>
        /// 今のステージのクリアカウントを静的データに代入する
        /// </summary>
        public void FetchData()
        {
            StatisticsDatastore.AssignTotalClearedStage(_clearedStageCount);
            StatisticsDatastore.AssignTotalDestroyedTargetBlock(_totalDestroyedTargetBlockCount);
        }
    }
}
