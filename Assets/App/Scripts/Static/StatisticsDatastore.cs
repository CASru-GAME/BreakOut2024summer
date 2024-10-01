using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace App.Static
{
    public static class StatisticsDatastore
    {
        public static int[] _totalAquiredItemNumberList = new int[6] { 0, 0, 0, 0, 0, 0 };
        public static int _totalAquiredExperiencePoint = 0;
        public static int _playerLevel = 1;
        public static int _totalClearedStage = 0;
        public static int _totalDestroyedTargetBlock = 0;

        ///<summery>
        /// 獲得したアイテムの数を代入する
        /// </summery>
        /// <param name="totalAquiredItemNumberList"></param>
        ///<remark>
        /// イテレータをidに対応させている。そのイテレータに対応する要素にアイテムの数を入力したリストを作り、それを代入する。
        /// </remark>
        /// <value>
        /// 6個の要素を持つint型の配列
        /// </value>
        public static void AssignTotalAquiredItemNumberList(int[] totalAquiredItemNumberList)
        {
            for (int i = 0; i < totalAquiredItemNumberList.Length; i++)
            {
                if (totalAquiredItemNumberList[i] < 0)
                {
                    UnityEngine.Debug.Log("TotalAquiredItemNumberList: 0より小さい値が入力されました");
                    return;
                }
            }
            _totalAquiredItemNumberList = totalAquiredItemNumberList;
        }

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
    }
}
