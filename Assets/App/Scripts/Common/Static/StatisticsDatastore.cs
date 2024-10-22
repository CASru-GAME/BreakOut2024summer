using System.Collections.Generic;

namespace App.Common.Static
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
        public static List<(int id, int stackCount)> _totalAquiredPerkList { get; private set; } = new List<(int id, int stackCount)>();
        ///<summary>
        /// クリアしたステージ数
        /// </summary>
        public static int _totalClearedStage { get; private set; } = 0;
        ///<summary>
        /// 助けた猫の数
        /// </summary>
        public static int _totalCat { get; private set; } = 0;
        ///<summary>
        /// 残りの制限時間
        /// </summary>
        public static float _remainingTimeLimit { get; private set; } = 0;
        ///<summary>
        /// 残りの残機
        /// </summary>
        public static int _remainingLive { get; private set; } = 0;
        ///<summary>
        /// 最大の残機
        /// </summary>
        public static int _maxLive { get; private set; } = 3;

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

        public static void AssignTotalAquiredPerkList(List<(int id, int stackCount)> totalAquiredPerkList)
        {
            _totalAquiredPerkList.Clear();
            _totalAquiredPerkList = totalAquiredPerkList;
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
        /// 助けた猫の数を代入する
        /// </summary>
        /// <param name="totalCat"></param>
        public static void AssignTotalCat(int totalCat)
        {
            if (totalCat < 0)
            {
                UnityEngine.Debug.Log("TotalCat: 0より小さい値が入力されました");
                return;
            }
            _totalCat = totalCat;
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
        /// 最大の残機を代入する
        /// </summary>
        /// <param name="maxLive"></param>
        public static void AssignMaxLive(int maxLive)
        {
            if (maxLive < 0)
            {
                UnityEngine.Debug.Log("MaxLive: 0より小さい値が入力されました");
                return;
            }
            _maxLive = maxLive;
        }

        

        /// <summary>
        /// 全ての統計データをリセットする
        /// </summary>
        public static void ResetAllStatisticsData()
        {
            _totalAquiredExperiencePoint = 0;
            _playerLevel = 1;
            _totalAquiredPerkList.Clear();
            _totalClearedStage = 0;
            _totalCat = 0;
            _remainingTimeLimit = 0;
            _remainingLive = 0;
        }
    }
}