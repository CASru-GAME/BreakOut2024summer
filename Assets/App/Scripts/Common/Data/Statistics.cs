using System.Collections.Generic;

namespace App.Common.Data
{
    [System.Serializable]
    public class StatisticsData
    {
        ///クリアした日付
        public string _clearedDate = "";
        /// クリアしたステージ数
        public int _totalClearedStage = 0;
        /// 助けた猫の数
        public int _totalCat = 0;
        /// 集めたパーク
        public List<(int id, int stackCount)> _totalAquiredPerkList = new List<(int id, int stackCount)>();

        public StatisticsData(string clearedDate, int totalClearedStage, int totalCat, List<(int id, int stackCount)> totalAquiredPerkList)
        {
            _clearedDate = clearedDate;
            _totalClearedStage = totalClearedStage;
            _totalCat = totalCat;
            _totalAquiredPerkList = totalAquiredPerkList;
        }
    }
}
