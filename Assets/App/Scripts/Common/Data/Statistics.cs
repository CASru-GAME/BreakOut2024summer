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
        public StatisticsData(string clearedDate, int totalClearedStage, int totalCat)
        {
            _clearedDate = clearedDate;
            _totalClearedStage = totalClearedStage;
            _totalCat = totalCat;
        }
    }
}
