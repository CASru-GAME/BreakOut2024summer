using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace App.Static
{
    public static class StatisticsDatastore
    {
        ///<summary>
        /// 獲得した経験値
        /// </summary>
        public static int _totalAquiredExperiencePoint { get; private set; } = 0;
        ///<summary>
        /// プレイヤーレベル
        /// </summary>
        public static int _playerLevel { get; private set; } = 1;
        ///<summary>
        /// 獲得したパークの数
        /// </summary>
        public static int[,] _totalAquiredPerkList { get; private set; } = new int[22, 2] { 
            { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, 
            { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, 
            { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } 
        };
        ///<summary>
        /// クリアしたステージ数
        /// </summary>
        public static int _totalClearedStage { get; private set; } = 0;
        ///<summary>
        /// 破壊したターゲットブロック数
        /// </summary>
        public static int _totalDestroyedTargetBlock { get; private set; } = 0;
        ///<summary>
        /// 残りの制限時間
        /// </summary>
        public static float _remainingTimeLimit { get; private set; } = 0;
        ///<summary>
        /// 残りの残機
        /// </summary>
        public static int _remainingLive { get; private set; } = 0;

        /// <summary>
        /// 獲得した経験値を代入する
        /// </summary>
        /// <param name="totalAquiredExperiencePoint"></param>
        public static void AssignTotalAquiredExperiencePoint(int totalAquiredExperiencePoint)
        {
            if (totalAquiredExperiencePoint < 0)
            {
                UnityEngine.Debug.Log("TotalAquiredExperiencePoint: 0より小さい値が入力されました");
                return;
            }
            _totalAquiredExperiencePoint = totalAquiredExperiencePoint;
        }

        /// <summary>
        /// プレイヤーレベルを代入する
        /// </summary>
        /// <param name="playerLevel"></param>
        public static void AssignPlayerLevel(int playerLevel)
        {
            if (playerLevel < 1)
            {
                UnityEngine.Debug.Log("PlayerLevel: 1より小さい値が入力されました");
                return;
            }
            _playerLevel = playerLevel;
        }

        /// <summary>
        /// 獲得したパークの数を代入する
        /// </summary>
        /// <param name="totalAquiredPerkList"></param>
        /// <remark>
        /// イテレータをidに対応させている。そのイテレータに対応する要素に取得したパークの総数を入力したリストを作り、それを代入する。
        /// </remark>
        /// <value>
        /// 22個の要素を持つint型の配列
        /// </value>
        public static void AssignTotalAquiredPerkList(int[,] totalAquiredPerkList)
        {
            for (int i = 0; i < totalAquiredPerkList.GetLength(0); i++)
            {
                if (totalAquiredPerkList[i, 0] < -1)
                {
                    UnityEngine.Debug.Log("TotalAquiredPerkList: -1より小さい値が入力されました");
                    return;
                }
            }
            _totalAquiredPerkList = totalAquiredPerkList;
        }

        /// <summary>
        /// クリアしたステージ数を代入する
        /// </summary>
        /// <param name="totalClearedStage"></param>
        public static void AssignTotalClearedStage(int totalClearedStage)
        {
            if (totalClearedStage < 0)
            {
                UnityEngine.Debug.Log("TotalClearedStage: 0より小さい値が入力されました");
                return;
            }
            _totalClearedStage = totalClearedStage;
        }

        /// <summary>
        /// 破壊したターゲットブロック数を代入する
        /// </summary>
        /// <param name="totalDestroyedTargetBlock"></param>
        public static void AssignTotalDestroyedTargetBlock(int totalDestroyedTargetBlock)
        {
            if (totalDestroyedTargetBlock < 0)
            {
                UnityEngine.Debug.Log("TotalDestroyedTargetBlock: 0より小さい値が入力されました");
                return;
            }
            _totalDestroyedTargetBlock = totalDestroyedTargetBlock;
        }

        /// <summary>
        /// 残りの制限時間を代入する
        /// </summary>
        /// <param name="remainingTimeLimit"></param>
        public static void AssignRemainingTimeLimit(float remainingTimeLimit)
        {
            if (remainingTimeLimit < 0)
            {
                UnityEngine.Debug.Log("RemainingTimeLimit: 0より小さい値が入力されました");
                return;
            }
            _remainingTimeLimit = remainingTimeLimit;
        }

        /// <summary>
        /// 残りの残機を代入する
        /// </summary>
        /// <param name="remainingLive"></param>
        public static void AssignRemainingLive(int remainingLive)
        {
            if (remainingLive < 0)
            {
                UnityEngine.Debug.Log("RemainingLive: 0より小さい値が入力されました");
                return;
            }
            _remainingLive = remainingLive;
        }

        /// <summary>
        /// 全ての統計データをリセットする
        /// </summary>
        public static void ResetAllStatisticsData()
        {
            _totalAquiredExperiencePoint = 0;
            _playerLevel = 1;
            _totalAquiredPerkList = new int[22, 2] {
                {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0},
                {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0},
                {0, 0}, {0, 0}
            };
            _totalClearedStage = 0;
            _totalDestroyedTargetBlock = 0;
            _remainingTimeLimit = 0;
            _remainingLive = 0;
        }
    }
}