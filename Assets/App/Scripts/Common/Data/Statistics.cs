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
        public List<AquiredPerk> _totalAquiredPerkList = new List<AquiredPerk>();

        public StatisticsData(string clearedDate, int totalClearedStage, int totalCat, List<(int id, int stackCount)> totalAquiredPerkList)
        {
            _clearedDate = clearedDate;
            _totalClearedStage = totalClearedStage;
            _totalCat = totalCat;
            foreach (var perk in totalAquiredPerkList)
            {
                _totalAquiredPerkList.Add(new AquiredPerk(perk.id, perk.stackCount));
            }
        }
    }

    [System.Serializable]
    public class AquiredPerk
    {
        public int _id = 0;
        public int _stackCount = 0;

        public AquiredPerk(int id, int stackCount)
        {
            _id = id;
            _stackCount = stackCount;
        }
    }
}
