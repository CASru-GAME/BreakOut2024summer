using System.Collections.Generic;

namespace App.Common.Data
{
    public class StatisticsData
    {
        ///クリアした日付
        public string _clearedDate { get; private set; } = "";
        /// クリアしたステージ数
        public int _totalClearedStage { get; private set; } = 0;
        /// 助けた猫の数
        public int _totalCat { get; private set; } = 0;
        /// 集めたパーク
        public List<(int id, int stackCount)> _totalAquiredPerkList { get; private set; } = new List<(int id, int stackCount)>();

        public StatisticsData(string clearedDate, int totalClearedStage, int totalCat, List<(int id, int stackCount)> totalAquiredPerkList)
        {
            _clearedDate = clearedDate;
            _totalClearedStage = totalClearedStage;
            _totalCat = totalCat;
            _totalAquiredPerkList = totalAquiredPerkList;
        }
    }
}
